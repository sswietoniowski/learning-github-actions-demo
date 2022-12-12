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
docker build -t sswietoniowski/todo-api:version3.0 -f backend/api/api-sqlite-with-volumes.dockerfile ./backend/api
```

## Running the API Docker image (locally)

To run the API, run the following command from the root of the repository:

```bash
docker run -p 5000:5000 -p 5001:5001 sswietoniowski/todo-api:version1.0 -d
docker run -p 5000:5000 -p 5001:5001 sswietoniowski/todo-api:version2.0 -d
docker run -p 5000:5000 -p 5001:5001 sswietoniowski/todo-api:version3.0 -d
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
