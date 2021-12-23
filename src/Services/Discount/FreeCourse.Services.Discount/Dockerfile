
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src

COPY ["src/Services/Discount/FreeCourse.Services.Discount/FreeCourse.Services.Discount.csproj", "src/Services/Discount/FreeCourse.Services.Discount/"]
COPY ["src/Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "src/Shared/FreeCourse.Shared/"]

RUN dotnet restore "src/Services/Discount/FreeCourse.Services.Discount/FreeCourse.Services.Discount.csproj"
COPY . .
WORKDIR "/src/src/Services/Discount/FreeCourse.Services.Discount"

RUN dotnet build "FreeCourse.Services.Discount.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Services.Discount.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Services.Discount.dll"]