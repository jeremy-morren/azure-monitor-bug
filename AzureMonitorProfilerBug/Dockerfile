FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
ENV APPLICATIONINSIGHTS_CONNECTION_STRING='InstrumentationKey=1e1103ef-c6d4-44fd-92d0-b42dfc1d3984;IngestionEndpoint=https://australiaeast-1.in.applicationinsights.azure.com/;LiveEndpoint=https://australiaeast.livediagnostics.monitor.azure.com/;ApplicationId=b385b131-e12f-4c5f-8d69-7c5e14a9bed6'

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY "AzureMonitorProfilerBug.csproj" ./
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AzureMonitorProfilerBug.dll"]
