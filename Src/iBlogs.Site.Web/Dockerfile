FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /Src
COPY ["Src/iBlogs.Site.Web/iBlogs.Site.Web.csproj", "Src/iBlogs.Site.Web/"]
RUN dotnet restore "Src/iBlogs.Site.Web/iBlogs.Site.Web.csproj"
COPY . .
WORKDIR "/Src/Src/iBlogs.Site.Web"
RUN dotnet build "iBlogs.Site.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "iBlogs.Site.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "iBlogs.Site.Web.dll"]
CMD ["argument"]