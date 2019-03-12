FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src/Monolith
COPY . .
RUN apt-get update && apt-get install -y gnupg2 \
	&& curl -sL https://deb.nodesource.com/setup_8.x | bash - \
	&& apt-get install -y nodejs \
	&& dotnet build AngularCore.csproj -c Release -o /app \
	&& dotnet publish AngularCore.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
COPY ./DockerCompose/docker-entrypoint.sh ./
WORKDIR /app/ClientApp
COPY ClientApp/ ./
RUN apt-get update && apt-get install -y gnupg2 \
	&& curl -sL https://deb.nodesource.com/setup_8.x | bash - \
	&& apt-get install -y nodejs \
	&& npm install
WORKDIR /app
ENTRYPOINT ["./docker-entrypoint.sh"]
