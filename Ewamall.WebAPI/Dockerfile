#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ewamall.WebAPI/File/Ewamall.apk", "Ewamall.WebAPI/"]
COPY ["Ewamall.WebAPI/Ewamall.WebAPI.csproj", "Ewamall.WebAPI/"]
RUN dotnet restore "Ewamall.WebAPI/Ewamall.WebAPI.csproj"
COPY . .
WORKDIR "/src/Ewamall.WebAPI"
RUN dotnet build "Ewamall.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ewamall.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ["Ewamall.WebAPI/File/Ewamall.apk", "app/"]
COPY ["Ewamall.WebAPI/File/Ewamall.apk", "./"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ewamall.WebAPI.dll"]