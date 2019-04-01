FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 15003
EXPOSE 15103

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ImageService/ImageService.csproj ImageService/
RUN dotnet restore ImageService/ImageService.csproj
COPY ./ImageService .
WORKDIR /src/ImageService
RUN dotnet build ImageService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ImageService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ImageService.dll"]
