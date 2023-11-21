FROM mcr.microsoft.com/dotnet/aspnet:8.0
LABEL name="CarterService"
COPY src/bin/Release/net8.0/publish/ App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "CarterService.dll"]
