#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
expose 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web/PersonalWebsiteBE/PersonalWebsiteBE.csproj", "Web/PersonalWebsiteBE/"]
COPY ["Infrastructure/PersonalWebsiteBE.Services/PersonalWebsiteBE.Services.csproj", "Infrastructure/PersonalWebsiteBE.Services/"]
COPY ["Persistence/PersonalWebsiteBE.Repository/PersonalWebsiteBE.Repository.csproj", "Persistence/PersonalWebsiteBE.Repository/"]
COPY ["Core/PersonalWebsiteBE.Core/PersonalWebsiteBE.Core.csproj", "Core/PersonalWebsiteBE.Core/"]
COPY ["Integrations/PersonalWebsiteBE.Sendgrid/PersonalWebsiteBE.Sendgrid.csproj", "Integrations/PersonalWebsiteBE.Sendgrid/"]
COPY ["Core/PersonalWebsiteBE.Extensions/PersonalWebsiteBE.Extensions.csproj", "Core/PersonalWebsiteBE.Extensions/"]
RUN dotnet restore "Web/PersonalWebsiteBE/PersonalWebsiteBE.csproj"
COPY . .
WORKDIR "/src/Web/PersonalWebsiteBE"
RUN dotnet build "PersonalWebsiteBE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonalWebsiteBE.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonalWebsiteBE.dll"]