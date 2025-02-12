FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copy everything
COPY . .

# Restore and publish the WebApp
RUN dotnet restore "WebApp/WebApp.csproj"
RUN dotnet publish -c Release "WebApp/WebApp.csproj" -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .

# Expose port 5193
EXPOSE 5193

ENTRYPOINT ["dotnet", "WebApp.dll"]