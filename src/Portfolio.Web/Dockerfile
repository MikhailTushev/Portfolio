FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443/tcp

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./src/Portfolio.Web/Portfolio.Web.csproj", "/src"]

RUN dotnet restore "Portfolio.Web.csproj"
COPY . .

WORKDIR "/src/"
RUN dotnet build "src/Portfolio.Web/Portfolio.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Portfolio.Web/Portfolio.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.Web.dll"]
