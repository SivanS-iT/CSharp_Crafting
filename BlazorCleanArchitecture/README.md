# Blazor clean arhitecture

The project is created for learning purposes and testing workflows in .NET enviroment.
This is a .NET 8 project following the [**Clean Architecture**](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).

## Endpoints

The API endpoints are built using [**ASP.NET Web API**]


## ---------------------- Technology Stack of application ----------------------
### Entity Framework Core

[**Entity Framework Core**](https://github.com/dotnet/efcore) is the used database ORM on this project.

### PostgreSQL

The database system used on the project is [**PostgreSQL**](https://www.postgresql.org/).

### Docker

[**Docker**](https://www.docker.com/) is used for containerization of database and its management system.

## Patterns

Design patterns help write better code and have numerous advantages.
Some of those are:

  1. Simplifying the coding process
  2. Better maintainability
  3. Code reuse

### CQRS

CQRS separates read and write operations for a database. It maximizes performance, scalability and security.
[**MediatR**](https://github.com/jbogard/MediatR) is used to achieve this.


### Repository

The repository pattern provides an abstraction layer between the application's data logic and
the data source. It promotes easier testing and maintainability.


## ---------------------- Testing ----------------------

The project also contains tests. There are unit tests, functional tests and integration tests are planed to be made.
Tests are written using [**xunit**](https://xunit.net/) and [**FluentAssertions**](https://fluentassertions.com/).

### Unit Tests

Unit tests are used for testing small pieces of code. Mocking is used to replace different services, like an external database. [**NSubstitute**](https://nsubstitute.github.io/) is used for mocking.

### Functional Tests *NOT INTEGRATED*

Functional tests are used for testing the application as a whole. Real services are used for these tests.

### Integration Tests *NOT INTEGRATED*

Integration tests check if multiple modules of the application work together as expected.
