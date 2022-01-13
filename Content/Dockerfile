FROM mcr.microsoft.com/dotnet/aspnet:6.0
LABEL name="CarterService"
COPY src/bin/Release/net6.0/publish/ App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "CarterService.dll"]
