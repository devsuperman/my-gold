FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

COPY ["App3/App3.csproj", "App3/"]
RUN dotnet restore "App3/App3.csproj"

COPY . ./
RUN dotnet publish "App3/App3.csproj" -c Release -o /out

FROM nginx:alpine AS final
COPY --from=build /out/wwwroot /usr/share/nginx/html