FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /Src
COPY ["src/iBlogs.Site.Web/iBlogs.Site.Web.csproj", "src/iBlogs.Site.Web/"]
RUN dotnet restore "src/iBlogs.Site.Web/iBlogs.Site.Web.csproj"
COPY . .
WORKDIR "/src/src/iBlogs.Site.Web"
RUN dotnet build "iBlogs.Site.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "iBlogs.Site.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "iBlogs.Site.Web.dll"]
CMD ["argument"]