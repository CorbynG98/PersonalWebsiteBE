#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
expose 8080

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PersonalWebsiteBE/PersonalWebsiteBE.csproj", "PersonalWebsiteBE/"]
COPY ["PersonalWebsiteBE.Core/PersonalWebsiteBE.Core.csproj", "PersonalWebsiteBE.Core/"]
COPY ["PersonalWebsiteBE.Services/PersonalWebsiteBE.Services.csproj", "PersonalWebsiteBE.Services/"]
COPY ["PersonalWebsiteBE.Repository/PersonalWebsiteBE.Repository.csproj", "PersonalWebsiteBE.Repository/"]
RUN dotnet restore "PersonalWebsiteBE/PersonalWebsiteBE.csproj"
COPY . .
WORKDIR "/src/PersonalWebsiteBE"
RUN dotnet build "PersonalWebsiteBE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonalWebsiteBE.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonalWebsiteBE.dll"]