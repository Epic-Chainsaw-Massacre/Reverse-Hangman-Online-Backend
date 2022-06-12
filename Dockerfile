FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /reverse-hangman-online-backend
COPY reverse-hangman-online-backend/ ./
ENTRYPOINT ["dotnet", "reverse-hangman-online-backend.dll"]
