#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5005

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["twatter-postservice/twatter-postservice.csproj", "twatter-postservice/"]
RUN dotnet restore "twatter-postservice/twatter-postservice.csproj"
COPY . .
WORKDIR "/src/twatter-postservice"
RUN dotnet build "twatter-postservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "twatter-postservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "twatter-postservice.dll"]
