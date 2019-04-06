FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
ADD /publish/* ./
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "whatsapp-api.dll"]