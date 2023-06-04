#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

  FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update && apt-get install -y git
RUN git clone https://github.com/kongkong56/testtest.git /app
RUN git submodule update --init --recursive


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["chatu-net-sdk/src/ChatUAISDK/ChatUAISDK.csproj", "."]
COPY ["testrobot/testrobot.csproj", "."]
RUN dotnet restore "./testrobot.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "testrobot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "testrobot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "testrobot.dll"]