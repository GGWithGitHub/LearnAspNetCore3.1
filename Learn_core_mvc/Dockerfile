#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Learn_core_mvc/Learn_core_mvc.csproj", "Learn_core_mvc/"]
COPY ["Learn_core_mvc.Core/Learn_core_mvc.Core.csproj", "Learn_core_mvc.Core/"]
COPY ["Learn_core_mvc.Repository/Learn_core_mvc.Repository.csproj", "Learn_core_mvc.Repository/"]
COPY ["Learn_core_mvc.Service/Learn_core_mvc.Service.csproj", "Learn_core_mvc.Service/"]
RUN dotnet restore "Learn_core_mvc/Learn_core_mvc.csproj"
COPY . .
WORKDIR "/src/Learn_core_mvc"
RUN dotnet build "Learn_core_mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Learn_core_mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Learn_core_mvc.dll"]