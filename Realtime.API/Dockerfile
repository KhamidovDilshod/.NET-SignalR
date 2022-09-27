FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY [".NET-SignalR/Realtime.API/Realtime.API.csproj", "Realtime.API/"]
RUN dotnet restore "Realtime.API/Realtime.API.csproj"
COPY . .
WORKDIR "/src/Realtime.API"
RUN dotnet build "Realtime.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Realtime.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Realtime.API.dll"]
