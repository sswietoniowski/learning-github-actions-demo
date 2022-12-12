# Sample Todo API

This is a sample Todo API that uses the ASP.NET Core Web API template.

## Building the API Docker image

To build basic Docker images for the API, run the following command from the root of the repository.

Version 1.0 (using SQLite database, no volumes configured)

```bash
docker build -t sswietoniowski/todo-api:version1.0 -f backend/api/api-sqlite.dockerfile ./backend/api
```

Version 2.0 (using SQLite database, volumes configured)

```bash
docker build -t sswietoniowski/todo-api:version2.0 -f backend/api/api-sqlite-with-volumes.dockerfile ./backend/api
```

Version 3.0 (using MSSQL database in a separate container)

```bash
docker build -t sswietoniowski/todo-api:version3.0 -f backend/api/api-mssql.dockerfile ./backend/api
```

## Running the API Docker image (locally)

To run the API, run the following command from the root of the repository:

```bash
docker run -p 5000:5000 -p 5001:5001 -d sswietoniowski/todo-api:version1.0
docker run -p 5000:5000 -p 5001:5001 -d sswietoniowski/todo-api:version2.0
```

In case of the MSSQL version, you need to run the MSSQL container first:

```bash
docker network create todo-app
docker volume create sqlvolume
docker run --network todo-app --network-alias mssql -p 2433:1433 -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password123!' -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-latest
```

To test connection to the MSSQL server from the app, run the following command:

```bash
docker run --network todo-app -it --rm mcr.microsoft.com/mssql-tools sqlcmd -S mssql -U SA -P Password123!
$Env:DatabaseEngine = "Mssql"
$Env:ConnectionStrings:MssqlConnection = "Server=localhost,2433;Database=todos;User=sa;Password=Password123!;TrustServerCertificate=true"
cd .\backend\api
dotnet run
```

Then, run the API container:

```bash
docker run -p 5000:5000 -p 5001:5001 --network todo-app -d sswietoniowski/todo-api:version3.0
```

## Pushing the API Docker image to Docker Hub

To push the API Docker image to Docker Hub, run the following command from the root of the repository:

```bash
docker push sswietoniowski/todo-api:version1.0
docker push sswietoniowski/todo-api:version2.0
docker push sswietoniowski/todo-api:version3.0
```

## Running the API Docker image (remotely)

To do so, execute the same commands as above, but do so from a [remote machine](https://labs.play-with-docker.com/).

## Running the API Docker image (locally) with Docker Compose

To run the API with Docker Compose, run the following command from the root of the repository:

```bash
docker-compose -f backend/api/docker-compose.yml up -d
```
