# To run project locally:

## Visual Studio
1. Clone the repository
2. Edit `appsettings.json` to update the parameters of  `ConnectionStrings:SqlServerDatabase` for the preferred Sql Server instance, and `Jwt:Key` to any newly generated key
3. To run the API, use the pre-configured `http` profile, and swagger UI will be launched in the browser
4. To run the unit tests, open Test Explorer in Visual Studio

## Docker
1. Clone the repository
2. Create a `.env` file following the example of `.env.example`, and the parameters of  `ConnectionStrings:SqlServerDatabase` for the preferred Sql Server instance, and `Jwt:Key` to any newly generated key
3. run `docker build -t zimozi-dotnet-api -f Zimozi-dotnet-api-assignment/Dockerfile .` from terminal to build the image
4. run `docker run -p 8080:8080 --env-file .env zimozi-dotnet-api`
5. Navigate to http://localhost:8080/swagger/index.html for swagger UI

# Usage



## Endpoints
All /api/tasks methods require authentication
Bearer token is obtained from the **/api/login** method
To execute requests in SwaggerUI, the bearer token returned is to be pasted after clikcing the `Authorize` button at the top right of the webpage.

### Available usernames:
Admin
Admin2
Admin3
User
User2
User3

### POST /api/login
Verifies username against users in database, and on successful verification returns a bearer token for accessing the other endpoints.


### GET /api/tasks
Fetches list of all tasks
### GET /api/tasks/{Id}
Fetches a task by provided Id
### Post /api/tasks
**Requires authorization as an Admin user**
Creates a new task, requires valid username.

