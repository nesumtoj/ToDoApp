# Use the .NET Core SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project files
COPY *.csproj ./
RUN dotnet restore

# Install curl
RUN apt-get update && apt-get install -y curl

# Copy everything else and build the application
COPY . ./
RUN dotnet publish -c Release -o out

# Use the ASP.NET Core runtime image for the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose the API port
EXPOSE 5000

# Set the entrypoint
ENTRYPOINT ["dotnet", "TaskManagement.dll"]