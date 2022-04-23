#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY ./ ./
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore "ARS.Inventory.Management/ARS.Inventory.Management.Web.csproj"
COPY . .
WORKDIR "/src/ARS.Inventory.Management"
RUN dotnet build "ARS.Inventory.Management.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ARS.Inventory.Management.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ARS.Inventory.Management.Web.dll"]