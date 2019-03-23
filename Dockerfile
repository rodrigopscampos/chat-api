FROM microsoft/dotnet:aspnetcore-runtime
EXPOSE 53351
EXPOSE 44399
ADD /app . /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "whatsapp-api.dll"]