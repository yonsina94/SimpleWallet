FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5000

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["SimpleWallet.Api/SimpleWallet.Api.csproj", "SimpleWallet.Api/"]
RUN dotnet restore "SimpleWallet.Api/SimpleWallet.Api.csproj"
COPY . .
WORKDIR "/src/SimpleWallet.Api"
RUN dotnet build "SimpleWallet.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "SimpleWallet.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleWallet.Api.dll"]
