FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
ENV TZ Africa/Johannesburg

FROM microsoft/dotnet:2.2-sdk AS build

WORKDIR /app
COPY ["Debian/wkhtmltox_0.12.6-1.stretch_amd64.deb", "/app"]

RUN mkdir pdf && chmod a+w pdf
RUN echo "deb http://ftp.us.debian.org/debian jessie main contrib" | tee -a /etc/apt/sources.list
RUN apt-get update && apt-get upgrade -y && apt-get install -y apt-utils && apt-get install -y ttf-mscorefonts-installer
RUN apt install -y ttf-mscorefonts-installer && apt install -y fontconfig
RUN apt install ./wkhtmltox_0.12.6-1.stretch_amd64.deb -y
RUN apt -f install
RUN wkhtmltopdf --version
 
WORKDIR /src
COPY ["andriesk-api-htmltopdf.csproj", "."]
COPY *.nupkg ./
COPY *.config ./

RUN dotnet restore -s https://api.nuget.org/v3/index.json   -s /src
COPY . .
RUN dotnet build "andriesk-api-htmltopdf.csproj" -c Release -o /app


FROM build AS publish
RUN dotnet publish "andriesk-api-htmltopdf.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app

COPY --from=publish /app .

ENTRYPOINT ["dotnet", "andriesk-api-htmltopdf.dll"]
