#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Catalog/FreeCourse.Services.Catalog/FreeCourse.Services.Catalog.csproj", "src/Services/Catalog/FreeCourse.Services.Catalog/"]
COPY ["src/Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "src/Shared/FreeCourse.Shared/"]
RUN dotnet restore "src/Services/Catalog/FreeCourse.Services.Catalog/FreeCourse.Services.Catalog.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/FreeCourse.Services.Catalog"
RUN dotnet build "FreeCourse.Services.Catalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Services.Catalog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Services.Catalog.dll"]