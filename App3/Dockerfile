FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5170

ENV ASPNETCORE_URLS=http://+:5170

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["App3/App3.csproj", "App3/"]
RUN dotnet restore "App3/App3.csproj"
COPY . .
WORKDIR "/src/App3"
RUN dotnet build "App3.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "App3.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

RUN rm -rf ./*
COPY --from=publish /app/publish/wwwroot .
COPY /App3/nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
