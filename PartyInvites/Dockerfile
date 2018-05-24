FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
COPY bin/Debug/netcoreapp2.0/publish /app
ENV ASPNETCORE_ENVIRONMENT="Development"
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "PartyInvites.dll"]
