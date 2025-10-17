FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/ContentService/ContentService.Web.Api/ContentService.Web.Api.csproj", "src/Services/ContentService/ContentService.Web.Api/"]
COPY ["src/Services/ContentService/ContentService.Domain/ContentService.Domain.csproj", "src/Services/ContentService/ContentService.Domain/"]
COPY ["src/Services/ContentService/ContentService.SharedKernel/ContentService.SharedKernel.csproj", "src/Services/ContentService/ContentService.SharedKernel/"]
COPY ["src/Services/ContentService/ContentService.Application/ContentService.Application.csproj", "src/Services/ContentService/ContentService.Application/"]
COPY ["src/Services/ContentService/ContentService.Infrastructure/ContentService.Infrastructure.csproj", "src/Services/ContentService/ContentService.Infrastructure/"]
RUN dotnet restore "src/Services/ContentService/ContentService.Web.Api/ContentService.Web.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/ContentService/ContentService.Web.Api"
RUN dotnet build "./ContentService.Web.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ContentService.Web.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContentService.Web.Api.dll"]
