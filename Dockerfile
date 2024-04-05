FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PomocKolezenska/PomocKolezenska.csproj", "PomocKolezenska/"]
RUN dotnet restore "PomocKolezenska/PomocKolezenska.csproj"
COPY . .
WORKDIR "/src/PomocKolezenska"
RUN dotnet build "PomocKolezenska.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PomocKolezenska.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PomocKolezenska.dll"]
