# publish code
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

## copy files 
WORKDIR /App
ADD ./DevicesManagement/DevicesManagement ./src/DevicesManagement
ADD ./DevicesManagement/Database ./src/Database
ADD ./DevicesManagement/Authentication ./src/Authentication
ADD ./DevicesManagement/DevicesManagement.sln ./src

WORKDIR /App/src/DevicesManagement
RUN dotnet restore
RUN dotnet publish -c Release -o ../../out

## install ef
#WORKDIR /App/src/Database
#RUN dotnet tool install -g dotnet-ef
#ENV PATH="$PATH:/root/.dotnet/tools"
#RUN dotnet ef migrations add InitDb -c DevicesManagementContext -o ./Migrations/DevicesDb -s ../DevicesManagement
#RUN dotnet ef database update -c DevicesManagementContext -s ../DevicesManagement
#RUN dotnet ef migrations add InitDb -c LocalAuthContext -o ./Migrations/AuthDb -s ../DevicesManagement
#RUN dotnet ef database update -c LocalAuthContext -s ../DevicesManagement

###

# create lightweight container
FROM mcr.microsoft.com/dotnet/aspnet:6.0

## copy built .dll files
WORKDIR /App/out
COPY --from=build ./App/out .

## init container
EXPOSE 5000
ENTRYPOINT ["dotnet", "DevicesManagement.dll", "--environment=Docker"]