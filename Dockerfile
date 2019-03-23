FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
EXPOSE 53351
EXPOSE 44399
ADD /app/* ./
ENTRYPOINT ["dotnet", "whatsapp-api.dll"]