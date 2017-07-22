# github-repo-browser-api
This is sample .NET Core 1.1 Web API project ready to be hosted on Heroku. It is used as a backend microservice for Angular4 UI project for browsing repositories. The backend uses **PostgreSQL** with **Entity Framework** and **Redis** for storing data and statistics.

## Running locally
Perform the following steps in Visual Studio Code or in the terminal:
1. `dotnet restore`
1. `dotnet run --db.connection.string YOUR_DATABASE --redis.connection.string YOUR_REDIS`
Note that `YOUR_DATABASE` parameter should be in the format Entity Framework expects. Refer to [this](http://www.npgsql.org/doc/connection-string-parameters.html) for details. Do not forget to set `SSL Mode=Require` for Heroku.

## Deployment to Heroku
Please specify .NET Core buildpack like shown [here](https://blog.jenyay.com/running-asp-net-core-in-heroku/).
