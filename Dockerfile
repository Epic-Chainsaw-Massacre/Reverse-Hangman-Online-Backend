# Build
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./Reverse-Hangman-Online-Backend/Reverse-Hangman-Online-Backend.csproj" --disable-parallel
RUN dotnet publish "./Reverse-Hangman-Online-Backend/Reverse-Hangman-Online-Backend.csproj" -c release -o /app --no-restore

# Run
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "Reverse-Hangman-Online-Backend.dll"] 
