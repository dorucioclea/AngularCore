FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 15002
EXPOSE 15102

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY PostService/PostService.csproj PostService/
RUN dotnet restore PostService/PostService.csproj
COPY ./PostService .
WORKDIR /src/PostService
RUN dotnet build PostService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PostService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PostService.dll"]
