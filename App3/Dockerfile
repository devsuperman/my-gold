FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build-env
RUN apk add nodejs
RUN apk add npm
WORKDIR /app
COPY . ./
RUN npm --prefix	CommonComponents install
RUN dotnet publish "App3/App3.csproj" -c Release -o output

FROM nginx:alpine
WORKDIR /user/share/nginx/html
COPY --from=build-env /app/output/wwwroot .
COPY /App3/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80