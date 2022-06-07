FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY src/*.csproj .
RUN dotnet restore

COPY src/. .
RUN dotnet publish -c release -o /app 

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build /app ./
ENTRYPOINT [ "dotnet","OneByte.dll" ]