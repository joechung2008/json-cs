# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["json-cs.sln", "./"]
COPY ["API/API.csproj", "API/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY API/. ./API/
COPY Shared/. ./Shared/
RUN dotnet restore "API/API.csproj"
RUN dotnet publish "API/API.csproj" -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5132
ENV ASPNETCORE_URLS=http://0.0.0.0:5132
ENTRYPOINT ["dotnet", "API.dll"]
