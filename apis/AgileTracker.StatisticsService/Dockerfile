#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["AgileTracker.StatisticsService.Startup/AgileTracker.StatisticsService.Startup.csproj", "AgileTracker.StatisticsService.Startup/"]
COPY ["AgileTracker.StatisticsService.Web/AgileTracker.StatisticsService.Web.csproj", "AgileTracker.StatisticsService.Web/"]
COPY ["application/AgileTracker.StatisticsService.Application/AgileTracker.StatisticsService.Application.csproj", "application/AgileTracker.StatisticsService.Application/"]
COPY ["infrastructure/AgileTracker.StatisticsService.Infrastructure/AgileTracker.StatisticsService.Infrastructure.csproj", "infrastructure/AgileTracker.StatisticsService.Infrastructure/"]
COPY ["domain/AgileTracker.StatisticsService.Domain/AgileTracker.StatisticsService.Domain.csproj", "domain/AgileTracker.StatisticsService.Domain/"]
RUN dotnet restore "AgileTracker.StatisticsService.Startup/AgileTracker.StatisticsService.Startup.csproj"
COPY . .
WORKDIR "/src/AgileTracker.StatisticsService.Startup"
RUN dotnet build "AgileTracker.StatisticsService.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgileTracker.StatisticsService.Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgileTracker.StatisticsService.Startup.dll"]