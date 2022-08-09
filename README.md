# Rewards application

`Rewards` application is created with C# and .NET 6

## System requirements

* .NET 6 - To build application locally without using Docker you will need .NET 6 SDK: <https://dotnet.microsoft.com/en-us/download/dotnet/6.0>
* Docker - to build a docker image you will need a docker installed (in this case you don't need to have .NET 6 SDK locally): <https://docs.docker.com/engine/install/>

## Local build

To build application with .NET SDK run the following command from the root of the repo:

```bash
#!/bin/bash
dotnet build
```

To start application run the following command from the root of the repo:

```bash
#!/bin/bash
dotnet run --project Rewards.Web
```

You will see a message which containы information about the used port:

```text
Now listening on: http://localhost:5285
```

Now you can open this url in browser: <http://localhost:5285/swagger/index.html> and test `/Report/ByCustomerAndMonth` method with following `POST` request:

```json
{
  "startDate": "2022-01-01T00:00:00Z"
}
```

Or you can use any tool (`curl`, `Postman`) that can post http requests to <http://localhost:5285/Report/ByCustomerAndMonth>

## Use Docker

To build docker image run this command from the root of the repo:

```bash
#!/bin/bash
docker build -f Rewards.Web/Dockerfile -t rewards .
```

To run the application in container run this command (replace port `8080` with another one if required):

```bash
#!/bin/bash
docker run -d -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Development --name rewards-app rewards

```

Now you can open this url in browser: <http://localhost:8080/swagger/index.html> and test `/Report/ByCustomerAndMonth` method with following `POST` request:

```json
{
  "startDate": "2022-01-01T00:00:00Z"
}
```

If you don't need Swagger, remove ASPNETCORE_ENVIRONMENT env var:

```bash
#!/bin/bash
docker run -d -p 8080:80 --name rewards-app rewards
```

In this case you will need any tool (`curl`, `Postman`) that can post http requests to <http://localhost:8080/Report/ByCustomerAndMonth>

To stop and remove container run this command:

```bash
#!/bin/bash
docker stop rewards-app && docker rm rewards-app
```
