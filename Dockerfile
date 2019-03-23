FROM microsoft/dotnet:aspnetcore-runtime
EXPOSE 53351
EXPOSE 44399
ADD /app/* /opt/app
ENTRYPOINT ["dotnet", "whatsapp-api.dll"]