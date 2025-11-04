
# Runtime image only — for use on internet-less servers
#FROM registry-gl.uzex.uz/sh.giyosov/dotnet/sdk:8.0 AS runtime

# Optional: use non-root user if required
#USER $APP_UID

#WORKDIR /app

# Port exposed by the app (can be adjusted as needed)
#EXPOSE 8080

# Copy already published output from CI
#COPY publish/ ./

# Start the application
#ENTRYPOINT ["dotnet", "UzEx.Analytics.Api.dll"]













# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
FROM registry-gl.uzex.uz/sh.giyosov/dotnet/sdk:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM registry-gl.uzex.uz/sh.giyosov/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/UzEx.Analytics.Api/UzEx.Analytics.Api.csproj", "src/UzEx.Analytics.Api/"]
COPY ["src/UzEx.Analytics.Application/UzEx.Analytics.Application.csproj", "src/UzEx.Analytics.Application/"]
COPY ["src/UzEx.Analytics.Domain/UzEx.Analytics.Domain.csproj", "src/UzEx.Analytics.Domain/"]
COPY ["src/UzEx.Analytics.Infrastructure/UzEx.Analytics.Infrastructure.csproj", "src/UzEx.Analytics.Infrastructure/"]
RUN dotnet restore "./src/UzEx.Analytics.Api/UzEx.Analytics.Api.csproj"
COPY . .
WORKDIR "/src/src/UzEx.Analytics.Api"
RUN dotnet build "./UzEx.Analytics.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UzEx.Analytics.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UzEx.Analytics.Api.dll"]