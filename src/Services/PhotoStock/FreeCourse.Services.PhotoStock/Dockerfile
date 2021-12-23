
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src

COPY ["src/Services/PhotoStock/FreeCourse.Services.PhotoStock/FreeCourse.Services.PhotoStock.csproj", "src/Services/PhotoStock/FreeCourse.Services.PhotoStock/"]
COPY ["src/Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "src/Shared/FreeCourse.Shared/"]

RUN dotnet restore "src/Services/PhotoStock/FreeCourse.Services.PhotoStock/FreeCourse.Services.PhotoStock.csproj"
COPY . .
WORKDIR "/src/src/Services/PhotoStock/FreeCourse.Services.PhotoStock"

RUN dotnet build "FreeCourse.Services.PhotoStock.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Services.PhotoStock.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Services.PhotoStock.dll"]