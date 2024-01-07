#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ControlServices.WebApi/ControlServices.WebApi.csproj", "ControlServices.WebApi/"]
COPY ["ControlServices.Infra/ControlServices.Infra.IoC/ControlServices.Infra.IoC.csproj", "ControlServices.Infra/ControlServices.Infra.IoC/"]
COPY ["ControlServices.Application/ControlServices.Application.Domain/ControlServices.Application.Domain.csproj", "ControlServices.Application/ControlServices.Application.Domain/"]
COPY ["ControlServices.Application/ControlServices.Application.Mediator/ControlServices.Application.Mediator.csproj", "ControlServices.Application/ControlServices.Application.Mediator/"]
COPY ["ControlServices.Infra/ControlServices.Infra.Data/ControlServices.Infra.Data.csproj", "ControlServices.Infra/ControlServices.Infra.Data/"]
RUN dotnet restore "./ControlServices.WebApi/./ControlServices.WebApi.csproj"
COPY . .
WORKDIR "/src/ControlServices.WebApi"
RUN dotnet build "./ControlServices.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ControlServices.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControlServices.WebApi.dll"]