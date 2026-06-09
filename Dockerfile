# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["football-backend.csproj", "./"]
RUN dotnet restore "./football-backend.csproj"

# Copy the remaining source code
COPY . .
WORKDIR "/src/."
RUN dotnet build "football-backend.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "football-backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official ASP.NET Core runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "football-backend.dll"]
