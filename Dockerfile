FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
EXPOSE 8080
ADD /publish/* ./
ENTRYPOINT ["dotnet", "whatsapp-api.dll"]