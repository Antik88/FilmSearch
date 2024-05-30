FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8003

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["FilmoSearch/FilmoSearch.csproj", "FilmoSearch/"]
RUN dotnet restore "FilmoSearch/FilmoSearch.csproj"

COPY . .
WORKDIR "/src/FilmoSearch"

RUN dotnet build "FilmoSearch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FilmoSearch.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FilmoSearch.dll"]

