FROM microsoft/dotnet:aspnetcore-runtime
EXPOSE 53351
EXPOSE 44399
ADD /app . /app
ENTRYPOINT ["dotnet", "whatsapp-web.dll"]