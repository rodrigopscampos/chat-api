# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY out .

ENV ASPNETCORE_URLS=http://+:5000
ENV BD__TIPO=memoria

ENTRYPOINT ["dotnet", "chat-api.dll"]