# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# WORKDIR /app
# EXPOSE 5173

# ENV ASPNETCORE_URLS=http://+:5173

# USER app
# FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG configuration=Release
# WORKDIR /src
# COPY ["App3/App3.csproj", "App3/"]
# RUN dotnet restore "App3/App3.csproj"
# COPY . .
# WORKDIR "/src/App3"
# RUN dotnet build "App3.csproj" -c $configuration -o /app/build

# FROM build AS publish
# ARG configuration=Release
# RUN dotnet publish "App3.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "App3.dll"]

# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos de projeto e restaurar as dependências
COPY ["App3/App3.csproj", "App3/"]
RUN dotnet restore "App3/App3.csproj"

# Copiar o restante dos arquivos e compilar a aplicação
COPY . .
RUN dotnet publish "App3/App3.csproj" -c Release -o /out

# Etapa 2: Servir a aplicação com Nginx
FROM nginx:alpine AS final
WORKDIR /app
COPY --from=build /out/wwwroot /usr/share/nginx/html
COPY /App3/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
