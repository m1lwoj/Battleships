FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Battleships.ConsoleUI/Battleships.ConsoleUI.csproj", "Battleships.ConsoleUI/"]
RUN dotnet restore "Battleships.ConsoleUI/Battleships.ConsoleUI.csproj"
COPY . .
WORKDIR "/src/Battleships.ConsoleUI"
RUN dotnet build "Battleships.ConsoleUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Battleships.ConsoleUI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Battleships.ConsoleUI.dll"]