# DevicesManagementApi
REST API for managing connected devices.

The project is not intended for commercial use. It's purpose is to learn and practice .net frameworks, especially in creating REST APIs.

### Overview:
The project consists of rest api with (currently only) Bearer token authorization using jwt. The users are separated into 2 roles: admins and the employees. The admin user can manage content in the system: devices, devices' commands and users. Employee user can create and delete commands that can be sent to one's devices. All data is stored in the database systems. Devices and network connecting the api to them is faked with message bus and C# programs. Assumption of the project is to store authentication context in separate database system than devices context (though not meaning external authentication providers).   

### Tech stack and protocols:
 - Framework used for the API: .net6
 - Devices' context database system: PostgresSQL
 - Authentication context database system: (currently) PostgresSQL
 - Message-bus RabbitMQ
 - Authentication protocole: Bearer token with jwt (currently only method)
 - API documentation: postman collection

### What is yet to be done:
 - Dockerizing the api itself.
 - Adding startup scripts with first admin user credentials and databases ports.
 - Refactor DeviceManagement database, so relation between devices and users is M:N.
 - Replace LocalAuth database to credentials only database.
 - Add docker container with message-bus and C# programs imitating network of devices. Integrate API to send and listen for
    messages in the network. 
 - Add refresh jwt endpoint.
 - Add cookies authentication.



