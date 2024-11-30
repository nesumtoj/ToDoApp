# Use an official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the application source code from the GitHub repository
RUN apt-get update && apt-get install -y git && \
    git clone https://github.com/nesumtoj/ToDoApp.git . && \
    cd backend

# Restore dependencies and build the application
RUN dotnet restore ./backend
RUN dotnet publish -c Release -o out ./backend

# Use a smaller runtime image for final execution
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Set the working directory for the runtime
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build-env /app/backend/out .

# Expose the API port
EXPOSE 5000

# Set the entrypoint
ENTRYPOINT ["dotnet", "TaskManagement.dll"]