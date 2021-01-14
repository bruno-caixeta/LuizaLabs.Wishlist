FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
COPY . .
ENTRYPOINT ["dotnet", "LuizaLabs.Wishlist.API.dll"]