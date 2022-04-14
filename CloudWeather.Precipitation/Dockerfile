#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./CloudWeather.Precipitation.csproj" --disable-parallel
RUN dotnet publish "./CloudWeather.Precipitation.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "CloudWeather.Precipitation.dll"]