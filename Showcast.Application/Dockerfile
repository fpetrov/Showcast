FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Showcast.Application/Showcast.Application.csproj", "Showcast.Application/"]
RUN dotnet restore "Showcast.Application/Showcast.Application.csproj"
COPY . .
WORKDIR "/src/Showcast.Application"
RUN dotnet build "Showcast.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Showcast.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Showcast.Application.dll"]
