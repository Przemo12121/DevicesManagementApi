#!/bin/sh

FIRST_START=$( grep "FIRST_START=[*^]*" .env | tr -d [:space:] | cut --delimiter='=' --fields=2 )

if [ $FIRST_START = "true" ]; then
    dotnet DatabaseInit/Database.dll "Username=devices;Password=testpassword;Server=device_db;Database=devices_menagement" "Username=devices_auth;Password=testpassword_auth;Server=auth_db;Database=devices_menagement_auth" aaaa12345678 testPWD123
    sed -i 's/FIRST_START=true/FIRST_START=false/' .env
fi

dotnet DevicesManagement.dll --environment=Docker 