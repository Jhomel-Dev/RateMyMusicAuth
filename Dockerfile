# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["RateMyMusicAuth.csproj", "./"]
RUN dotnet restore "RateMyMusicAuth.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "RateMyMusicAuth.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "RateMyMusicAuth.csproj" -c Release -o /app/publish

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080

# Environment variables for ASP.NET
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "RateMyMusicAuth.dll"]
