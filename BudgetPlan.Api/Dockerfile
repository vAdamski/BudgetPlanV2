FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BudgetPlan.Api/BudgetPlan.Api.csproj", "BudgetPlan.Api/"]
COPY ["BudgetPlan.Application/BudgetPlan.Application.csproj", "BudgetPlan.Application/"]
COPY ["BudgetPlan.Domain/BudgetPlan.Domain.csproj", "BudgetPlan.Domain/"]
COPY ["BudgetPlan.Infrastructure/BudgetPlan.Infrastructure.csproj", "BudgetPlan.Infrastructure/"]
COPY ["BudgetPlan.Persistence/BudgetPlan.Persistence.csproj", "BudgetPlan.Persistence/"]
RUN dotnet restore "BudgetPlan.Api/BudgetPlan.Api.csproj"
COPY . .
WORKDIR "/src/BudgetPlan.Api"
RUN dotnet build "./BudgetPlan.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BudgetPlan.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BudgetPlan.Api.dll"]
