FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ECommerce.slnx ./
COPY Domain/*.csproj ./Domain/
COPY Application/*.csproj ./Application/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY EComerceWebApi/*.csproj ./EComerceWebApi/

RUN dotnet restore ./EComerceWebApi/EComerceWebApi.csproj

COPY . .
RUN dotnet publish ./EComerceWebApi/EComerceWebApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "EComerceWebApi.dll"]