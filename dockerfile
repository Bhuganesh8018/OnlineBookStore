# Use official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the full project
COPY . ./

# Build the application
RUN dotnet publish -c Release -o /app/publish

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Start the app
ENTRYPOINT ["dotnet", "OnlineBookStore.dll"]
  
