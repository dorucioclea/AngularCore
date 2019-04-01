FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 16000
EXPOSE 16100

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Gateways/ClientGateway/ClientGateway.csproj Gateways/ClientGateway/
RUN dotnet restore Gateways/ClientGateway/ClientGateway.csproj
COPY . .
WORKDIR /src/Gateways/ClientGateway
RUN dotnet build ClientGateway.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ClientGateway.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ClientGateway.dll"]
