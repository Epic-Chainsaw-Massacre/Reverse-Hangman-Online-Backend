FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /reverse-hangman-online-backend
COPY Reverse-Hangman-Online-Backend/ ./
ENTRYPOINT ["dotnet", "reverse-hangman-online-backend.dll"]
