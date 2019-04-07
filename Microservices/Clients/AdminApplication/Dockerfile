FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5001

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY AdminApplication.csproj AdminApplication/
RUN dotnet restore AdminApplication/AdminApplication.csproj
COPY . AdminApplication/
WORKDIR /src/AdminApplication
RUN rm -r ClientApp/dist && dotnet build AdminApplication.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AdminApplication.csproj -c Release -o /app

FROM node AS nodebuilder
RUN mkdir /usr/src/app
WORKDIR /usr/src/app
ENV PATH /usr/src/app/node_modules/.bin:$PATH
COPY ClientApp/package.json /usr/src/app/package.json
RUN npm install && npm install -g @angular/cli@7.1.2 --unsafe
COPY ClientApp/. /usr/src/app/
RUN rm -r dist && npm run build

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
RUN rm -r ClientApp/dist && mkdir -p /app/ClientApp/dist
COPY --from=nodebuilder /usr/src/app/dist /app/ClientApp/dist
ENTRYPOINT ["dotnet", "AdminApplication.dll"]
