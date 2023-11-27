FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BloodRush.DonationFacility.API/BloodRush.DonationFacility.API.csproj", "src/BloodRush.DonationFacility.API/"]
RUN dotnet restore "src/BloodRush.DonationFacility.API/BloodRush.DonationFacility.API.csproj"
COPY . .
WORKDIR "/src/src/BloodRush.DonationFacility.API"
RUN dotnet build "BloodRush.DonationFacility.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BloodRush.DonationFacility.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BloodRush.DonationFacility.API.dll"]
