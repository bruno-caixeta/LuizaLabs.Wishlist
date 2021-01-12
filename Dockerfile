FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 7000
COPY . .
ENTRYPOINT ["dotnet", "BadMediaAnalysis.API.dll"]