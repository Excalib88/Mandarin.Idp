FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Mandarin.Idp.WebTest/Mandarin.Idp.WebTest.csproj", "Mandarin.Idp.WebTest/"]
RUN dotnet restore "Mandarin.Idp.WebTest/Mandarin.Idp.WebTest.csproj"
COPY . .
WORKDIR "/src/Mandarin.Idp.WebTest"
RUN dotnet build "Mandarin.Idp.WebTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mandarin.Idp.WebTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mandarin.Idp.WebTest.dll"]
