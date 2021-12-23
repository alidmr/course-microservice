
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src

COPY ["src/Services/Basket/FreeCourse.Services.Basket/FreeCourse.Services.Basket.csproj", "src/Services/Basket/FreeCourse.Services.Basket/"]
COPY ["src/Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "src/Shared/FreeCourse.Shared/"]

RUN dotnet restore "src/Services/Basket/FreeCourse.Services.Basket/FreeCourse.Services.Basket.csproj"
COPY . .
WORKDIR "/src/src/Services/Basket/FreeCourse.Services.Basket"

RUN dotnet build "FreeCourse.Services.Basket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Services.Basket.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Services.Basket.dll"]