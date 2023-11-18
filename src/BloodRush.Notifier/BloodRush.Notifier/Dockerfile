FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BloodRush.Notifier/BloodRush.Notifier/BloodRush.Notifier.csproj", "src/BloodRush.Notifier/BloodRush.Notifier/"]
RUN dotnet restore "src/BloodRush.Notifier/BloodRush.Notifier/BloodRush.Notifier.csproj"
COPY . .
WORKDIR "/src/src/BloodRush.Notifier/BloodRush.Notifier"
RUN dotnet build "BloodRush.Notifier.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BloodRush.Notifier.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BloodRush.Notifier.dll"]
