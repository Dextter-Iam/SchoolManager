# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY MVCCamiloMentoria/*.csproj ./MVCCamiloMentoria/
RUN dotnet restore ./MVCCamiloMentoria/MVCCamiloMentoria.csproj

COPY . .
WORKDIR /src/MVCCamiloMentoria
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "MVCCamiloMentoria.dll"]
