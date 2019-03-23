FROM microsoft/dotnet:aspnetcore-runtime as base
WORKDIR /app
EXPOSE 53351
EXPOSE 44399

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "whatsapp-api.dll"]