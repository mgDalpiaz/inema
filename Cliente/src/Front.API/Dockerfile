FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Front.API/Front.API.csproj", "src/Front.API/"]
RUN dotnet restore "src/Front.API/Front.API.csproj"
COPY . .
WORKDIR "/src/src/Front.API"
RUN dotnet build "Front.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Front.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Front.API.dll"]