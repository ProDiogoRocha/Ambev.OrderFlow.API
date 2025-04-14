# Etapa de construção
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 6000
EXPOSE 6001

# Etapa de publicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Ambev.OrderFlow.Api/Ambev.OrderFlow.Api.csproj", "Ambev.OrderFlow.Api/"]
RUN dotnet restore "Ambev.OrderFlow.Api/Ambev.OrderFlow.Api.csproj"
COPY . .
WORKDIR "/src/Ambev.OrderFlow.Api"
RUN dotnet build "Ambev.OrderFlow.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ambev.OrderFlow.Api.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ambev.OrderFlow.Api.dll"]