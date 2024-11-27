FROM mcr.microsoft.com/dotnet/aspnet:9.0
LABEL name="CarterService"
COPY src/bin/Release/net9.0/publish/ App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "CarterService.dll"]
