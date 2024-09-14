# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos de projeto e restaurar as dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante dos arquivos e compilar a aplicação
COPY . ./
RUN dotnet publish -c Release -o /out

# Etapa 2: Servir a aplicação com Nginx
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Remover arquivos padrão do Nginx e copiar os arquivos da aplicação
RUN rm -rf ./*
COPY --from=build /out/wwwroot .

# Expor a porta 80 para o acesso ao container
EXPOSE 80
