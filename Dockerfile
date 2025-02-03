# Use official .NET SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY . ./
RUN dotnet restore

# Build and publish the app
RUN dotnet publish -c Release -o /publish

# Use a smaller runtime image for deployment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /publish .

# Expose port 8080 (or whatever your app uses)
EXPOSE 8080

# Set the entry point
ENTRYPOINT ["dotnet", "BookStore.Web.Api.dll"]
