FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /reverse-hangman-online-backend
COPY Reverse-Hangman-Online-Backend/ ./
ENTRYPOINT ["dotnet", "reverse-hangman-online-backend.dll"]
