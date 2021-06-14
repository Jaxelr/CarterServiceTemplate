FROM mcr.microsoft.com/dotnet/aspnet:5.0
LABEL name="CarterService"
COPY src/bin/Release/net5.0/publish/ App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "CarterService.dll"]
