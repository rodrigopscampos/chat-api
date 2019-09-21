FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app

COPY out /app/out

#Configura algumas variáveis de ambiente (configuração da aplicação)
ENV ASPNETCORE_URLS=http://+:5000
ENV BD__TIPO=memoria

#Expecifica que a inicialização da aplicação é dada por dotnet chat-api.dll
ENTRYPOINT ["dotnet", "chat-api.dll"]