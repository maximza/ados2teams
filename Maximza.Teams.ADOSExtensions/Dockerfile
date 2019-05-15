#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1809 AS build
WORKDIR /src
COPY ["Maximza.Teams.ADOSExtensions/Maximza.Teams.ADOSExtensions.csproj", "Maximza.Teams.ADOSExtensions/"]
RUN dotnet restore "Maximza.Teams.ADOSExtensions/Maximza.Teams.ADOSExtensions.csproj"
COPY . .
WORKDIR "/src/Maximza.Teams.ADOSExtensions"
RUN dotnet build "Maximza.Teams.ADOSExtensions.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Maximza.Teams.ADOSExtensions.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Maximza.Teams.ADOSExtensions.dll"]