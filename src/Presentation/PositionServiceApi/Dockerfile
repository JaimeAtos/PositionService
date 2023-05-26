#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["PositionService/", "PositionService/"]
COPY ["Atos.Core/", "Atos.Core/"]

RUN dotnet restore "PositionService/src/Presentation/PositionServiceApi/PositionServiceApi.csproj"

COPY PositionService/ .
COPY Atos.Core/ .

WORKDIR "/src/PositionService/src/Presentation/PositionServiceApi"
RUN dotnet build "PositionServiceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PositionServiceApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ARG dbpass
ARG dbuser
ARG dbhost
ARG dbport
ARG dbname

ARG mquser
ARG mqpass
ARG mqhost
ARG mqport

ENV MQPASSWORD=$mqpass
ENV MQUSER=$mquser
ENV MQHOST=$mqhost
ENV MQPORT=$mqport

ENV DBPASSWORD=$dbpass
ENV DBUSER=$dbuser
ENV DBHOST=$dbhost
ENV DBPORT=$dbport
ENV DBNAME=$dbname

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PositionServiceApi.dll"]
