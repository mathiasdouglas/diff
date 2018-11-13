# API DIFF [![Codacy Badge](https://api.codacy.com/project/badge/Grade/583d26fc38824a479be665d5293e7c72)](https://www.codacy.com/app/mathiasdouglas/diff?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=mathiasdouglas/diff&amp;utm_campaign=Badge_Grade)[![Build status](https://ci.appveyor.com/api/projects/status/shjyapmtnf9jdykv?svg=true)](https://ci.appveyor.com/project/mathiasdouglas/diff)

Steps run this API:

1. download MongoDB: https://www.mongodb.com/dr/fastdl.mongodb.org/win32/mongodb-win32-x86_64-2008plus-ssl-4.0.1-signed.msi/download

2. Install MongoDB

3. Clone/download the repository (master branch) and run the API locally using the project WaesDiff.API as a startup project

> The API documentation is on the swagger part

## API Usage Guide
This document describes how to interact with the API

### About the API
The API will respond to HTTP requests with the requested data in JSON format. When receiving POST requests, it expects to receive a payload formatted in JSON.

This API was made to perform the comparison between two strings with JSON base64 encoded binary data, and returns where the difference(s) occur, and what the size of the difference. For this purpose, it has 3 endpoints, of which 2 for post representing the strings for comparison (right and left) and one for GET, to obtain the result of the comparison.

**Exemple of POST:**
  > <host>/v1/diff/<ID>/left
  > <host>/v1/diff/<ID>/right
  
**Exemple of GET:**
  > <host>/v1/diff/<ID>

*Important: The GET will only return the diff if it is called before the right and left Post with correct values.*

**Exemple of Rertun**
{
    "message": "There are differences between the two json, see details:",
    "detail": [
        {
            "offset": 1,
            "length": 1
        },
        {
            "offset": 61,
            "length": 6
        }
    ]
}

## References
* [microsoft-microservices-architecture](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)

* [strategy-pattern](https://adamstorr.azurewebsites.net/blog/aspnetcore-and-the-strategy-pattern)

* [microsoft-integration-tests](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-2.1)

## Useful Links
* [base64-to-hex](https://cryptii.com/pipes/base64-to-hex)
