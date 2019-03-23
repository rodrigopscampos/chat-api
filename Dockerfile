FROM microsoft/dotnet:aspnetcore-runtime as base
WORKDIR /app
EXPOSE 53351
EXPOSE 44399

FROM microsoft/dotnet:sdk AS build
WORKDIR /src
COPY ["AspNetCoreMvcEcommerce/AspNetCoreMvcEcommerce.csproj", "AspNetCoreMvcEcommerce/"]
RUN dotnet restore "AspNetCoreMvcEcommerce/AspNetCoreMvcEcommerce.csproj"
COPY . .
WORKDIR "/src/AspNetCoreMvcEcommerce"
RUN dotnet build "AspNetCoreMvcEcommerce.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AspNetCoreMvcEcommerce.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AspNetCoreMvcEcommerce.dll"]