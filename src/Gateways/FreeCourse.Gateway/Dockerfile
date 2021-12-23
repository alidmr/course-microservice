FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src

COPY ["src/Gateways/FreeCourse.Gateway/FreeCourse.Gateway.csproj", "src/Gateways/FreeCourse.Gateway/"]
RUN dotnet restore "src/Gateways/FreeCourse.Gateway/FreeCourse.Gateway.csproj"
COPY . .

WORKDIR "/src/src/Gateways/FreeCourse.Gateway"

RUN dotnet build "FreeCourse.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Gateway.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Gateway.dll"]