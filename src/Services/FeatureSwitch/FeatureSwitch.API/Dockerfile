#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/FeatureSwitch/FeatureSwitch.API/FeatureSwitch.API.csproj", "Services/FeatureSwitch/FeatureSwitch.API/"]
COPY ["Services/Product/Product.API/Product.API.csproj", "Services/Product/Product.API/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
COPY ["Services/Product/Product.Infrastructure/Product.Infrastructure.csproj", "Services/Product/Product.Infrastructure/"]
COPY ["Services/Product/Product.Domain/Product.Domain.csproj", "Services/Product/Product.Domain/"]
COPY ["Services/Product/Product.Application/Product.Application.csproj", "Services/Product/Product.Application/"]
RUN dotnet restore "Services/FeatureSwitch/FeatureSwitch.API/FeatureSwitch.API.csproj"
COPY . .
WORKDIR "/src/Services/FeatureSwitch/FeatureSwitch.API"
RUN dotnet build "FeatureSwitch.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FeatureSwitch.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeatureSwitch.API.dll"]
