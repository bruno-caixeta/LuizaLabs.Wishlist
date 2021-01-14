# LuizaLabs.Wishlist
This project uses .Net Core 3.1 and PostgreSQL.

The easiest way to run is by setting up a PostgreSQL instance and create a database called Wishlist with user: postgres password: postgres.

Entity Framework migrations will be applied automatically when running the project.

Project includes Swagger to make testing more convenient. To access Swagger, go to https://localhost:5001/swagger

Due to authentication, make sure to call login route first to obtain your token. Then, click on the authorize button and type "Bearer <YOUR_TOKEN>".

Since the complete login system was not required, only two static users were set:
User: green 
Password: go

User: red
Password: stop
