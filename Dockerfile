# Use the .NET 6 SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /source

# Copy the .NET project files and restore dependencies
COPY . .

# Restore dependencies for all projects in the solution
RUN dotnet restore "./AccountsApi/AccountsApi.csproj" --disable-parallel

# Publish
RUN dotnet publish "./AccountsApi/AccountsApi.csproj" -c release -o /app --no-restore

# Set the runtime base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

COPY --from=build /app ./


# Expose the desired port
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "AccountsApi.dll"]
