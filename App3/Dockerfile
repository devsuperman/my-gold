FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY /App3/App3.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY /App3/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80