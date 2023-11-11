#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Authentication.WebApi/Authentication.WebApi.csproj", "Authentication.WebApi/"]
COPY ["Authentication.Infra/Authentication.Infra.IoC/Authentication.Infra.IoC.csproj", "Authentication.Infra/Authentication.Infra.IoC/"]
COPY ["Authentication.Application/Authentication.Application.Domain/Authentication.Application.Domain.csproj", "Authentication.Application/Authentication.Application.Domain/"]
COPY ["Authentication.Application/Authentication.Application.Mediator/Authentication.Application.Mediator.csproj", "Authentication.Application/Authentication.Application.Mediator/"]
COPY ["Authentication.Infra/Authentication.Infra.Data/Authentication.Infra.Data.csproj", "Authentication.Infra/Authentication.Infra.Data/"]
RUN dotnet restore "Authentication.WebApi/Authentication.WebApi.csproj"
COPY . .
WORKDIR "/src/Authentication.WebApi"
RUN dotnet build "Authentication.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.WebApi.dll"]