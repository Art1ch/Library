FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

RUN mkdir -p /app/keys && \
    chown app:app /app/keys
ENV ASPNETCORE_DATA_PROTECTION__KEYRING_PATH=/app/keys \
    ASPNETCORE_DATA_PROTECTION__APPLICATIONNAME=LibraryService

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Library/LibraryService.API/LibraryService.API.csproj", "Library/LibraryService.API/"]
COPY ["Library/LibraryService.Application/LibraryService.Application.csproj", "Library/LibraryService.Application/"]
COPY ["Library/LibraryService.Core/LibraryService.Core.csproj", "Library/LibraryService.Core/"]
COPY ["Library/LibraryService.Infrastructure/LibraryService.Infrastructure.csproj", "Library/LibraryService.Infrastructure/"]
RUN dotnet restore "Library/LibraryService.API/LibraryService.API.csproj"
COPY . .
WORKDIR "/src/Library/LibraryService.API"
RUN dotnet build "LibraryService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibraryService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryService.API.dll"]