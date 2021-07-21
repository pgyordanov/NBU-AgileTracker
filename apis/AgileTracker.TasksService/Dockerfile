#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["AgileTracker.TasksService.Startup/AgileTracker.TasksService.Startup.csproj", "AgileTracker.TasksService.Startup/"]
COPY ["AgileTracker.TasksService.Web/AgileTracker.TasksService.Web.csproj", "AgileTracker.TasksService.Web/"]
COPY ["application/AgileTracker.TasksService.Application/AgileTracker.TasksService.Application.csproj", "application/AgileTracker.TasksService.Application/"]
COPY ["infrastructure/AgileTracker.TasksService.Infrastructure/AgileTracker.TasksService.Infrastructure.csproj", "infrastructure/AgileTracker.TasksService.Infrastructure/"]
COPY ["domain/AgileTracker.TasksService.Domain/AgileTracker.TasksService.Domain.csproj", "domain/AgileTracker.TasksService.Domain/"]
RUN dotnet restore "AgileTracker.TasksService.Startup/AgileTracker.TasksService.Startup.csproj"
COPY . .
WORKDIR "/src/AgileTracker.TasksService.Startup"
RUN dotnet build "AgileTracker.TasksService.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgileTracker.TasksService.Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgileTracker.TasksService.Startup.dll"]