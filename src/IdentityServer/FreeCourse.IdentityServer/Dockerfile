FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/IdentityServer/FreeCourse.IdentityServer/FreeCourse.IdentityServer.csproj", "src/IdentityServer/FreeCourse.IdentityServer/"]
COPY ["src/Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "src/Shared/FreeCourse.Shared/"]
RUN dotnet restore "src/IdentityServer/FreeCourse.IdentityServer/FreeCourse.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/IdentityServer/FreeCourse.IdentityServer"
RUN dotnet build "FreeCourse.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.IdentityServer.dll"]
