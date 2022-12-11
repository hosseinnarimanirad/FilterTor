#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 9595
EXPOSE 8585

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SampleApp/SampleApp.Api/SampleApp.Api.csproj", "SampleApp/SampleApp.Api/"]
COPY ["SampleApp/Application/SampleApp.Application/SampleApp.Application.csproj", "SampleApp/Application/SampleApp.Application/"]
COPY ["FilterTor/FilterTor.csproj", "FilterTor/"]
COPY ["SampleApp/Core/SampleApp.Core.FilterTor/SampleApp.Core.FilterTor.csproj", "SampleApp/Core/SampleApp.Core.FilterTor/"]
COPY ["SampleApp/Core/SampleApp.Core/SampleApp.Core.csproj", "SampleApp/Core/SampleApp.Core/"]
COPY ["SampleApp/Infrastructure/SampleApp.Messaging.Grpc/SampleApp.Messaging.Grpc.csproj", "SampleApp/Infrastructure/SampleApp.Messaging.Grpc/"]
COPY ["SampleApp/Infrastructure/SampleApp.Messaging.Kafka/SampleApp.Messaging.Kafka.csproj", "SampleApp/Infrastructure/SampleApp.Messaging.Kafka/"]
COPY ["SampleApp/Infrastructure/SampleApp.Persistence.Ef/SampleApp.Persistence.Ef.csproj", "SampleApp/Infrastructure/SampleApp.Persistence.Ef/"]
COPY ["SampleApp/Presentation/SampleApp.Presentation/SampleApp.Presentation.csproj", "SampleApp/Presentation/SampleApp.Presentation/"]
RUN dotnet restore "SampleApp/SampleApp.Api/SampleApp.Api.csproj"
COPY . .
WORKDIR "/src/SampleApp/SampleApp.Api"
RUN dotnet build "SampleApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SampleApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false
COPY --from=build /app/build/SabaTamin.Extensions.Swagger.xml  /app/publish/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SampleApp.Api.dll"]