FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Library/LibraryService.API/LibraryService.API.csproj", "Library/LibraryService.API/"]
COPY ["Library/LibraryService.Application/LibraryService.Application.csproj", "Library/LibraryService.Application/"]
COPY ["Library/LibraryService.Core/LibraryService.Core.csproj", "Library/LibraryService.Core/"]
COPY ["Library/LibraryService.Infrastructure/LibraryService.Infrastructure.csproj", "Library/LibraryService.Infrastructure/"]

RUN dotnet restore "Library/LibraryService.API/LibraryService.API.csproj"

COPY . .

WORKDIR "/src/Library/LibraryService.API"
RUN dotnet build "LibraryService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LibraryService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryService.API.dll"]
