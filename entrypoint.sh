#!/bin/sh

export ASPNETCORE_ConnectionStrings__DevicesDb="$1"
export ASPNETCORE_ConnectionStrings__AuthDb="$2"

ADMIN_EMPLOYEE_ID="$3"
ADMIN_PASSWORD="$4"

META_FILE=".meta"

if ! [ -e "$META_FILE" ]; then
    echo "FIRST_START=true" > "$META_FILE"
fi

FIRST_START=$( grep "FIRST_START=[*^]*" "$META_FILE" | tr -d [:space:] | cut --delimiter='=' --fields=2 )

if [ $FIRST_START = "true" ]; then
    dotnet DatabaseInit/Database.dll "$ASPNETCORE_ConnectionStrings__DevicesDb" "$ASPNETCORE_ConnectionStrings__AuthDb" "$ADMIN_EMPLOYEE_ID" "$ADMIN_PASSWORD"
    sed -i 's/FIRST_START=true/FIRST_START=false/' "$META_FILE"
fi

dotnet DevicesManagement.dll