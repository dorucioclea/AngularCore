FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 15001
EXPOSE 15101

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY SearchService/SearchService.csproj SearchService/
RUN dotnet restore SearchService/SearchService.csproj
COPY ./SearchService .
WORKDIR /src/SearchService
RUN dotnet build SearchService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SearchService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SearchService.dll"]
