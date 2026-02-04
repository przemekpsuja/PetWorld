# Step 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY ["PetWorld.Web/PetWorld.Web.csproj", "PetWorld.Web/"]
COPY ["PetWorld.Application/PetWorld.Application.csproj", "PetWorld.Application/"]
COPY ["PetWorld.Infrastructure/PetWorld.Infrastructure.csproj", "PetWorld.Infrastructure/"]
COPY ["PetWorld.Domain/PetWorld.Domain.csproj", "PetWorld.Domain/"]

# Restore dependencies
RUN dotnet restore "PetWorld.Web/PetWorld.Web.csproj"

# Copy all source files
COPY . .

# Build
WORKDIR "/src/PetWorld.Web"
RUN dotnet build "PetWorld.Web.csproj" -c Release -o /app/build

# Step 2: Publish
FROM build AS publish
RUN dotnet publish "PetWorld.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Step 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetWorld.Web.dll"]