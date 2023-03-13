# Warehouse MicroService Solution

.NET 6, ASP.NET Core

This solution is a collection of three microservices with different database (SQL Server, MySQL & MongoDB). It is developed with API Gateway, using Ocelot, which communicates with all the three microservices for doing the CRUD operations. Finally, a Web Application is added which only communicates with the API Gateway. Postman is used to test communication between the API Gateway. All authorization is implemented through API Gateway. Only /api/Order is with RateLimitOptions of requests, as limit of 3 per 60s. Run the application through Docker compose. Authentication and Authorization is done with Json Web Tokens.

Docker hub: https://hub.docker.com/repositories/donciavas

NuGet packages were used:

1. Microsoft.EntityFrameworkCore
2. Microsoft.EntityFrameworkCore.SqlServer
3. Microsoft.Extensions.DependencyInjection.Abstractions
4. Microsoft.EntityFrameworkCore.Relational
5. Microsoft.AspNetCore.Authentication.JwtBearer
6. Microsoft.IdentityModel.Tokens
7. Microsoft.VisualStudio.Azure.Containers.Tools.Targets
8. MongoDB.Driver
9. MySql.EntityFrameworkCore
10. System.IdentityModel.Tokens.Jwt
11. Ocelot
12. MimeKit
13. MailKit
14. Blazored.SessionStorage

---Important Notice--- What needs to be done in this program:

1. Finish creating Register and Login services (have only log in service without users database). 
2. Use Mail Service for a better puspose, such as comfirming my email address. 
3. Change Session Storage JWT encoding, as it is not safe. 
4. Create Unit tests. 
