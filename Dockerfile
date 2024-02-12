# publish code
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

## copy project files 
WORKDIR /App
ADD ./DevicesManagement/DevicesManagement ./src/DevicesManagement
ADD ./DevicesManagement/Database ./src/Database
ADD ./DevicesManagement/Authentication ./src/Authentication
ADD ./DevicesManagement/DevicesManagement.sln ./src

WORKDIR /App/src/DevicesManagement
RUN dotnet restore
RUN dotnet publish -c Release -o ../../out

WORKDIR /App/src/Database
RUN dotnet restore
RUN dotnet publish -c Release -o ../../out/DatabaseInit

# create lightweight container
FROM mcr.microsoft.com/dotnet/aspnet:6.0

ARG DEVICES_DB_CONNECTION_STRING
ENV devices_db=$DEVICES_DB_CONNECTION_STRING

ARG AUTH_DB_CONNECTION_STRING
ENV auth_db=$AUTH_DB_CONNECTION_STRING

ARG ADMIN_EMPLOYEE_ID
ENV eid=$ADMIN_EMPLOYEE_ID

ARG ADMIN_PASSWORD
ENV pwd=$ADMIN_PASSWORD

ARG API_PORT
ENV port=$API_PORT

## copy built .dll files
WORKDIR /App/out
COPY --from=build ./App/out .
ADD entrypoint.sh .
ADD .env .

## init container
EXPOSE $port
ENTRYPOINT ./entrypoint.sh $devices_db $auth_db $eid $pwd