FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 16001
EXPOSE 16101

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Gateways/AdminGateway/AdminGateway.csproj Gateways/AdminGateway/
RUN dotnet restore Gateways/AdminGateway/AdminGateway.csproj
COPY . .
WORKDIR /src/Gateways/AdminGateway
RUN dotnet build AdminGateway.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AdminGateway.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AdminGateway.dll"]

