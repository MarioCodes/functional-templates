#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 18796
# HTTP Only
ENV ASPNETCORE_URLS=http://+:18796;
# HTTPS Support. Gives me problems. See: https://docs.microsoft.com/es-es/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0
# EXPOSE 35209
# ENV ASPNETCORE_URLS=http://+:18796;https://+:35209
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["csharp-net-core-3.1-DI.csproj", "."]
RUN dotnet restore "./csharp-net-core-3.1-DI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "csharp-net-core-3.1-DI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "csharp-net-core-3.1-DI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "csharp-net-core-3.1-DI.dll"]