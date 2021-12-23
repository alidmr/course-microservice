FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["src/Services/Order/FreeCourse.Services.Order.API/FreeCourse.Services.Order.API.csproj", "src/Services/Order/FreeCourse.Services.Order.API/"]
COPY ["src/Services/Order/FreeCourse.Services.Order.Application/FreeCourse.Services.Order.Application.csproj", "src/Services/Order/FreeCourse.Services.Order.Application/"]
COPY ["src/Services/Order/FreeCourse.Services.Order.Infrastructure/FreeCourse.Services.Order.Infrastructure.csproj", "src/Services/Order/FreeCourse.Services.Order.Infrastructure/"]
COPY ["src/Services/Order/FreeCourse.Services.Order.Domain/FreeCourse.Services.Order.Domain.csproj", "src/Services/Order/FreeCourse.Services.Order.Domain/"]
COPY ["src/Services/Order/FreeCourse.Services.Order.Domain.Core/FreeCourse.Services.Order.Domain.Core.csproj", "src/Services/Order/FreeCourse.Services.Order.Domain.Core/"]
COPY ["src/Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "src/Shared/FreeCourse.Shared/"]

RUN dotnet restore "src/Services/Order/FreeCourse.Services.Order.API/FreeCourse.Services.Order.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Order/FreeCourse.Services.Order.API"

RUN dotnet build "FreeCourse.Services.Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Services.Order.API.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Services.Order.API.dll"]