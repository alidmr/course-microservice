FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["src/Frontends/FreeCourse.Web/FreeCourse.Web.csproj", "src/Frontends/FreeCourse.Web/"]
COPY ["src/Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "src/Shared/FreeCourse.Shared/"]

RUN dotnet restore "src/Frontends/FreeCourse.Web/FreeCourse.Web.csproj"
COPY . .
WORKDIR "/src/src/Frontends/FreeCourse.Web"

RUN dotnet build "FreeCourse.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Web.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Web.dll"]
