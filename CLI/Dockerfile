# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["json-cs.sln", "./"]
COPY ["CLI/CLI.csproj", "CLI/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY CLI/. ./CLI/
COPY Shared/. ./Shared/
RUN dotnet restore "CLI/CLI.csproj"
RUN dotnet publish "CLI/CLI.csproj" -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CLI.dll"]
