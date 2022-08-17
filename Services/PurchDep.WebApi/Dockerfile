#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/PurchDep.WebApi/PurchDep.WebApi.csproj", "Services/PurchDep.WebApi/"]
COPY ["Services/PurchDep.Interfaces.Base/PurchDep.Interfaces.Base.csproj", "Services/PurchDep.Interfaces.Base/"]
COPY ["Data/PurchDep.Dal/PurchDep.Dal.csproj", "Data/PurchDep.Dal/"]
COPY ["Services/PurchDep.Interfaces/PurchDep.Interfaces.csproj", "Services/PurchDep.Interfaces/"]
COPY ["Common/PurchDep.Domain.Base/PurchDep.Domain.Base.csproj", "Common/PurchDep.Domain.Base/"]
COPY ["Common/PurchDep.Domain/PurchDep.Domain.csproj", "Common/PurchDep.Domain/"]
COPY ["Data/PurchDep.Dal.InMemory/PurchDep.Dal.InMemory.csproj", "Data/PurchDep.Dal.InMemory/"]
RUN dotnet restore "Services/PurchDep.WebApi/PurchDep.WebApi.csproj"
COPY . .
WORKDIR "/src/Services/PurchDep.WebApi"
RUN dotnet build "PurchDep.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PurchDep.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PurchDep.WebApi.dll"]