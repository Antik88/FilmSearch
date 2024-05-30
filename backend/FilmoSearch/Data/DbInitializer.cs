using FilmoSearch.Models;

namespace FilmoSearch.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; 
            }

            var user = new User
            {
                Name = "admin",
                Password = "$2a$11$g/l.hHpEzbqAocXCGCFSnO01yJ8SYLBjoI.z3IsBMml.vW4E6DG.W",
                Role = "admin"
            };

            context.Users.Add(user);
            context.SaveChangesAsync();
        }
    }
}
