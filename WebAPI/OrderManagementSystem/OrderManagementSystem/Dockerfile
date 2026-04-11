FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

COPY ["OrderManagementSystem.csproj", "."]
RUN dotnet restore "OrderManagementSystem.csproj"

COPY . .
RUN dotnet build "OrderManagementSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderManagementSystem.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OrderManagementSystem.dll"]