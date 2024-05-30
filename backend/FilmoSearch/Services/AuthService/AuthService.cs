using AutoMapper;
using FilmoSearch.Data;
using FilmoSearch.DTOs.UserDtos;
using FilmoSearch.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FilmoSearch.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        private const string _baseRole = "User";

        public AuthService(AppDbContext context,
            IConfiguration configuration,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<string> Login(CreateUserDto createUserDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == createUserDto.Name);

            if (user == null || !BCrypt.Net.BCrypt.Verify(createUserDto.Password, user.Password))
            {
                return null;
            }

            string token = CreateUserToken(user);

            return token;
        }

        private string CreateUserToken(User user)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, _baseRole),
            };

            if (user.Role != _baseRole)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes("this is my custom Secret key for authentication"));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private string GenerateJwt(IEnumerable<Claim> claims, DateTime expirationDate)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> Register(CreateUserDto createUserDto)
        {
            string passwordHash =
                BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

            var user = _mapper.Map<User>(createUserDto);

            user.Password = passwordHash;
            user.Role = _baseRole;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = CreateUserToken(user);
            return token;
        }

        public async Task<string> CheckToken()
        {
            string token = _contextAccessor.HttpContext.Request.Headers["Authorization"]
                .ToString().Split(' ')[1];

            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                if (jwtToken.ValidTo.Subtract(DateTime.UtcNow) <= TimeSpan.FromDays(1))
                {
                    var expirationDate = DateTime.UtcNow.AddDays(2);
                    var newToken = GenerateJwt(jwtToken.Claims, expirationDate);

                    return newToken;
                }
                else
                {
                    return token;
                }
            }
            else
            {
                _contextAccessor.HttpContext.Response.StatusCode = 401;
                await _contextAccessor.HttpContext.Response.WriteAsync("Invalid authorization token");
                return null;
            }
        }
        public string GetName()
        {
            var result = string.Empty;
            if (_contextAccessor.HttpContext is not null)
            {
                result = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                var roles = _contextAccessor.HttpContext.User
                    .Claims.Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList();
                result += $" role: {string.Join(", ", roles)}";
            }

            return result;
        }
    }
}

