# Etapa 1: Build da aplicação Blazor WebAssembly
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos do projeto e restaura as dependências
COPY /App3/App3.csproj /App3
RUN dotnet restore /App3/App3.csproj

# Copia os arquivos restantes e publica a aplicação
COPY . ./
RUN dotnet publish /App3/App3.csproj -c Release -o /out

# Etapa 2: Imagem final com Nginx para servir a aplicação
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Remove o conteúdo padrão do Nginx e copia os arquivos da aplicação Blazor WebAssembly
RUN rm -rf ./*
COPY --from=build /out/wwwroot .

# Copia o arquivo de configuração personalizado do Nginx
COPY /App3/nginx.conf /etc/nginx/conf.d/default.conf

# Expõe a porta 80 (Railway automaticamente mapeia as portas necessárias)
EXPOSE 80

# Comando para iniciar o Nginx
CMD ["nginx", "-g", "daemon off;"]