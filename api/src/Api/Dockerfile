#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

<<<<<<< Updated upstream
FROM mcr.microsoft.com/dotnet/core/aspnet:5.0-buster-slim AS base
=======
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
>>>>>>> Stashed changes
WORKDIR /app
EXPOSE 80
EXPOSE 443

<<<<<<< Updated upstream
FROM mcr.microsoft.com/dotnet/core/sdk:5.0-buster AS build
=======
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
>>>>>>> Stashed changes
WORKDIR /src
COPY ["Api/Api.csproj", "Api/"]
COPY ["ApplicationContract/ApplicationContract.csproj", "ApplicationContract/"]
COPY ["Application/ApplicationService.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Api/Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
