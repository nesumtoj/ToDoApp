# Use an official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Install Git and clone the repository
RUN apt-get update && apt-get install -y git && \
    git clone https://github.com/nesumtoj/ToDoApp.git .

# Set the working directory to the backend project
WORKDIR /app/backend


# Restore dependencies and build the application
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# Use a smaller runtime image for final execution
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Set the working directory for the runtime
WORKDIR /app

# Install curl
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copy the published output from the build stage
COPY --from=build-env /app/out .

# Expose the API port
EXPOSE 8081

# Set the entrypoint
ENTRYPOINT ["dotnet", "TaskManagement.dll"]