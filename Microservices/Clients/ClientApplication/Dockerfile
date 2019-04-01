FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ClientApplication.csproj ClientApplication/
RUN dotnet restore ClientApplication/ClientApplication.csproj
COPY . ClientApplication.
WORKDIR /src/ClientApplication
RUN dotnet build ClientApplication.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ClientApplication.csproj -c Release -o /app

FROM node AS nodebuilder
RUN mkdir /usr/src/app
WORKDIR /usr/src/app
ENV PATH /usr/src/app/node_modules/.bin:$PATH
COPY ClientApp/package.json /usr/src/app/package.json
RUN npm install
RUN npm install -g @angular/cli@7.1.2 --unsafe
COPY ClientApp/. /usr/src/app
RUN npm run build

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
RUN mkdir -p /app/ClientApp/dist
COPY --from=nodebuilder /usr/src/app/dist/. /app/ClientApp/dist/
ENTRYPOINT ["dotnet", "ClientApplication.dll"]
