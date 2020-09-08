# Solen.Api

> This project represents the `API` or the « back end » of [Solen LMS](https://github.solenlms.com) solution.
> It's built using `ASP.NET Core` and following the principles of [Clean Architecture](https://www.amazon.com/Clean-Architecture-Craftsmans-Software-Structure/dp/0134494164) by [Robert C. Martin (aka Uncle Bob)](http://cleancoder.com).

# Table of Contents

- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Getting the source code](#getting-the-source-code)
  - [Application Settings](#application-settings)
  - [Building and running the application](#Building-and-running-the-application)
- [Solen API Discovery](#solen-API-discovery)
  - [Swagger (OpenAPI)](#Swagger-OpenAPI)
  - [Living documentation](#living-documentation)
  - [Solen SPA](#solen-SPA)
- [Clean Architecture (Quick Overview)](#clean-Architecture-Quick-Overview)
  - [Introduction](#introduction)
  - [The Dependency Rule](#the-dependency-Rule)
  - [Characteristics of a _Clean Architecture_](#characteristics-of-a-clean-architecture)
- [Architecture of `Solen API`](#architecture-of-Solen-API)
  - [Overview](#overview)
  - [Domain Layer](#domain-layer)
  - [Application Layer](#application-layer)
  - [Infrastructure Layer](#infrastructure-layer)
    - [Persistence sub-layer](#persistence-sub-layer)
    - [Infrastructure sub-layer](#infrastructure-sub-layer)
  - [Presentation Layer](#presentation-layer)
    - [_Thin_ Controller vs _Fat_ Controller](#thin-controller-vs-fat-controller)
  - [Main](#main)
  - [Basic Use Case process flow](#basic-use-case-process-flow)
- [Automated Tests](#automated-tests)

  - [Unit Tests](#unit-tests)
  - [Acceptance Tests](#acceptance-tests)

- [Contribution](#contribution)

# Getting Started

## Prerequisites

What you need to be installed in your machine to run this app :

- The latest [NET Core SDK](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/doc/refman/8.0/en/installing.html)

## Getting the source code

First, you should get the source code from the [GitHub repository](https://github.com/imanys/Solen.Api). You can either clone the repo or just download it as a `Zip` file.

```bash
# clone the repo
git clone https://github.com/imanys/Solen.Api.git
```

## Application Settings

The configuration file `appsettings.json` is not checked into the repository. Instead, there is a template file called `appsettings.template.json` located in `Src\Main\Solen` directory which you can rename to `appsettings.json`.

<details><summary>Click here to see all the settings</summary>
<p>

```jsonc
{
  "AppSettings": {
    // The application uses SignalR to send Web Socket notifications. In order to enable Cross-Origin Requests (CORS), SignalR requires to specify the expected origins.
    // The default value corresponds to the address used by Solen-SPA App (https://github.com/imanys/Solen-SPA) when it's locally installed.
    "CorsOrigins": "http://localhost:4200"
  },
  "ConnectionStrings": {
    "Default": "Server=localhost; Database=Solen-db; Uid=app-user; Pwd=app-user-password"
  },
  "Serilog": {
    "Using": [],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Information",
        "System": "Warning"
      }
    },
    "Enrich": [
      "FromLogContext",
      "WithMachineName",
      "WithProcessId",
      "WithThreadId"
    ],
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "Seq",
        "Args": {
          "serverUrl": "http://localhost:8081"
        }
      }
    ]
  },
  "Security": {
    "Key": "JWT Tokens secret key", // The secret key used to generate JWT Tokens

    "JwtTokenExpiryTimeInMinutes": "1", // JWT Token expiry time in minutes. JWT Tokens should be short-lived
    "RefreshTokenExpiryTimeInDays": "1", // Refresh Token expiry time in days.
    "IsSigninUpEnabled": true // Indicates whether or not the signing up process should be enabled
  },
  "AllowedHosts": "*",
  "SwaggerOptions": {
    "Enable": true, // Indicates whether or not swagger should be available
    "Url": "/swagger/v1/swagger.json",
    "Name": "Solen LMS API"
  },
  "EmailSettings": {
    "ApiKey": "sendGrid API Key", // Solen API uses this field as the SendGrid key to send emails
    "From": "no-reply@address.com",
    "IsPickupDirectory": true, // When it's set to true, emails are generated in a Pickup directory. Otherwise, the emails are sent by SendGrid.
    "PickupDirectory": "C:\\Temp\\SolenMails" // The path to the Pickup directory
  },
  // Local storage settings. By default, the app uses the local storage to store resources
  "LocalStorageSettings": {
    "BaseUrl": "http://localhost:5000",
    "ResourcesFolder": "Resources",
    "ImagesFolder": "Images",
    "VideosFolder": "Videos",
    "RawFolder": "Raw"
  },
  "CompleteOrganizationSigningUpPageInfo": {
    "Url": "http://localhost:4200/signing-up/organization/complete",
    "TokenParameterName": "token"
  },
  "CompleteUserSigningUpPageInfo": {
    "Url": "http://localhost:4200/signing-up/user",
    "TokenParameterName": "token"
  },
  "ResetPasswordPageInfo": {
    "Url": "http://localhost:4200/auth/reset",
    "TokenParameterName": "token"
  }
}
```

</p>
</details>

:bomb: Since the application checks whether the database is up to date with all `EF Core migrations` at the startup time, you should define a valid `ConnectionStrings` before running the app.

## Building and running the application

```bash
# change directory to the « Main » folder
cd Solen.Api/Src/Main/Solen

# build the solution
dotnet build Solen.Api.sln

# run the solution
dotnet run
```

The application should now be listening on `http://localhost:5000` :

![](https://user-images.githubusercontent.com/52765247/91731023-c9be1e80-eba6-11ea-8a95-943475b847aa.png)

# Solen API Discovery

There are three ways to « discover » `Solen API` :

## Swagger (OpenAPI)

A great way to discover and tests `APIs`, is using `Swagger / OpenAPI`. To access the `Swagger UI` via a browser, run `Solen API` application, access the application's URL and add `/swagger` at the end of it. The `swagger UI` should show up :

<details><summary>Click here to see the screenshot
</summary>
<p>

![](https://user-images.githubusercontent.com/52765247/91856985-92637680-ec67-11ea-997a-fdb0391dc468.png)

</p>
</details>

:warning: make sur that `Swagger` is enabled (`"Enable": true`) in the `SwaggerOptions` section of the `appsettings.json` file.

## Living documentation

As the application uses [Specflow](https://specflow.org) (a framework to support `BDD` for `.NET` applications), we can use tools like [Pickles](https://www.picklesdoc.com) to generate a living documentation using the specifications written in `Gherkin`. To access this documentation, navigate to [https://doc.api.solenlms.com](https://doc.api.solenlms.com).

:warning: The application is not fully covered by `Automated Acceptance Tests`, thus the living documentation is not yet complete!

## Solen SPA

Finally, you can install [Solen SPA](https://github.com/imanys/Solen-SPA), the `Angular` « front end » application of `Solen LMS`, and check out how `Solen SPA` makes calls to `Solen API`.

# Clean Architecture (Quick Overview)

## Introduction

As stated at the beginning of this document, the `Solen API` architecture is inspired by the _Clean Architecture_.

> _The idea of Clean Architecture, is to put the Business Logic and Rules (aka `Policies`) at the centre of the design, and put the `Infrastructure` (aka `mechanisms`) at the edges of this design._

<div style="text-align:center">
<img src="https://user-images.githubusercontent.com/52765247/92224033-92e75180-eea1-11ea-8d48-16d6eadb8b11.png" />
</div>

The Business Rules are divided between two layers: the `Domain` layer (aka _Entities_) and the `Application` layer (aka _Use Cases_). The `Domain` layer contains the enterprise business rules, and the `Application` layer contains the application business rules. The difference being that enterprise rules could be shared with other systems whereas application rules would typically be specific to this system.

These two layers form what is called the `Core` of the system.

## The Dependency Rule

What makes this architecture work is that all dependencies must flow inwards. The `Core` of the system has no dependencies on any outside layers. `Infrastructure`, `Persistence`... depend on `Core`, but not on one another.

> _Source code dependencies must point only inward, toward high-level policies._

This is the architectural application of the `Dependecy Inversion Principle`.

## Characteristics of a _Clean Architecture_

A _Clean Architecture_ produces systems that have the following characteristics :

- _Independent of frameworks_. `Core` should not be dependent on external frameworks such as `Entity Framework`.
- _Testable_. The business rules can be tested without the UI, database, web server, or any other external element.
- _Independent of the UI_. The UI can change easily. For example, we can swap out the `Web UI` for a `Console UI`, or `Angular` for `React`. Logic is contained within `Core`, so changing the UI will not impact the system.
- _Independent of the database_. We can change `SQL Server` for `Oracle`, `Mongo`, or something else. `Core` is not bound to the database.
- _Independent of any external agency_. `Core` simply doesn't know anything about the outside world.

For further reading about _Clean Architecture_, I highly recommend this [book](https://www.amazon.com/Clean-Architecture-Craftsmans-Software-Structure/dp/0134494164).

# Architecture of `Solen API`

## Overview

![](https://user-images.githubusercontent.com/52765247/92253258-91cd1900-eecf-11ea-8a0d-c7392946677c.PNG)

## Domain Layer

This layer contains the _Entities_ of `Solen API`. There are business objects that contain reusable Business Logic.
There are absolutely _NOT_ simple data structures. This layer is independent of data access concerns and has no dependencies.

<details><summary>Click here to see the `Domain Layer` structure
</summary>
<p>

![](https://user-images.githubusercontent.com/52765247/92253771-35b6c480-eed0-11ea-8a58-dff107f47fe2.PNG)

</p>
</details>

## Application Layer

<details><summary>Click here to see the `Application Layer` structure
</summary>
<p>

![](https://user-images.githubusercontent.com/52765247/92254037-96460180-eed0-11ea-8f41-a0fd10ecae2f.PNG)

</p>
</details>

### Use Cases

This layer encapsulates and implements all the Use Cases of `Solen API`. In general, each Use Case is independent of the others (`Single Responsibility Principle`). \
For example, in the `Users Management` « module », modifying or deleting the Use Case `InviteMembers` will have absolutely no effects on the `BlockUser` Use Case.

### CQRS Pattern

To tackle business complexity and keep use cases simple to read and maintain, the `Application` layer implements the architectural pattern [CQRS](https://martinfowler.com/bliki/CQRS.html). Using this pattern means clear separation between _Commands_ (Write operations) and _Queries_ (Read operations). This separation gives us, we developers, a clear picture of what pieces of code change the state of the application.

<details><summary>Click here to see an example of Use Cases  (Commands and Queries) </summary>
<p>

![](https://user-images.githubusercontent.com/52765247/92256765-6ac51600-eed4-11ea-8f7f-7839fbaaffa0.PNG)

</p>
</details>

### Mediator Pattern

To keep the application business rules out the external layers and to prevent this layers from knowing much about the Business Logic,
the `Application` layer implements the [Mediator Pattern](https://en.wikipedia.org/wiki/Mediator_pattern).

### MediatR Library

To implement the `CQRS` Pattern and the `Mediator` Pattern easily, the `Application` layer makes use of an open source
.NET library called [MediatR](https://github.com/jbogard/MediatR). It allows in-process messaging and provides an elegant and
powerful approach for writing `CQRS`.
When starting using `MediatR`, whe should first define a `Request`. A `Request` can be either a `Query` or a `Command`.

<details><summary>Click here to see an example of a Request</summary>
<p>

![](https://user-images.githubusercontent.com/52765247/92457394-765e5880-f1c4-11ea-8d49-8604189a32cd.PNG)

</p>
</details>

Once a `Request` is created, we need a `Handler` to **execute** the `Request`.

<details><summary>Click here to see an example of a Request Handler</summary>
<p>

![](https://user-images.githubusercontent.com/52765247/92458177-5a0eeb80-f1c5-11ea-896d-ebf79f116ab0.PNG)

</p>
</details>

### Requests Validation

When exposing public `APIs`, it is important to validate each incoming request to ensure that it meets all expected pre-conditions.
The system should process valid requests but return an error for any invalid requests. \
The validation process is part of the application business logic. Therefore, the responsibility for validating requests does not
belong within the `Web API` or `Console UI` or whatsoever external interfaces, but rather in the `Application` layer. \
To make the validation process easier, we make use of a popular .NET library for building validation rules called [FLUENT VALIDATION](https://fluentvalidation.net/).
The other advantage of using this library, is we can make use of `MediatR` pipeline behaviours to validate automatically every
request that requires validation before further processing occurs.

<details><summary>Click here to see an example of a Request Validator</summary>
<p>

![](https://user-images.githubusercontent.com/52765247/92459237-b6bed600-f1c6-11ea-811e-e31497d72a66.PNG)

</p>
</details>

## Infrastructure layer

The `Infrastructure` layer contains by two sub-layers : The `Persistence` sub-layer and the `Infrastructure` sub-layer.

### Persistence sub-layer

The `Persistence` layer is typically the implementations of all _repository interfaces_ defined in the `Application` layer.

The layer is actually implemented based on `Entity Framework Core`. All the configurations related to `EF Core` are also implemented in this layer.

### Infrastructure sub-layer

The `Infrastructure` sub-layer implements all interfaces (other than `repositories` ) from the `Application` layer to provide
functionality to access external systems or to perform some technical tasks :

- Sending notifications (Web Sockets with `SignalR`, Emails with `SendGrid`)
- Accessing resource files (on File System or on the Cloud)
- `MediatR` pipeline behaviours
- Security (Passwords generator, JWT Tokens generator...)
- ...

## Presentation layer

The `Presentation` layer is the entry point to the system from the user’s point of view. Its primary concerns are routing requests to the `Application` layer.
In the context of `Solen API`, the `Presentation` layer contains `ASP.NET Core Web APIs`. All the concerns related to the `GUIs` are handled by [Solen SPA](https://github.com/imanys/Solen-SPA) application.

### _Thin_ Controller vs _Fat_ Controller

In the "traditional" way to write controllers, we usually implement some business logic flow in like as Validation,
Mapping Objects, Return HTTP status code...

Example of a _Fat_ Controller :

```csharp
[HttpPost]
public async Task<IHttpActionResult> CreateCourse(CourseModel model) {
    if (!ModelState.IsValid) return BadRequest (ModelState);

    var course = new Course {
        Title = model.title
    };

    var result = await _coursesService.CreateCourse(course);

    if (!result.Succeeded) return GetErrorResult (result);

    return Ok ();
}
```

Using the _Clean Architecture_ (where all the business logic and rules are implemented in the `Core` layer),
and a library like `Mediadtr`, we can write controllers with few lines of code.
With such _dumb_ controllers, we have no need to test them.

![](https://user-images.githubusercontent.com/52765247/92463244-d9072280-f1cb-11ea-9eba-3ed9dea306a9.PNG)

## Main

The `Main` is just the entry point to the application and where all configurations are registered (Dependency Injection, Security...).

## Basic Use Case process flow

![](https://user-images.githubusercontent.com/52765247/92493823-3f546b00-f1f5-11ea-814c-7f1e65c66b4c.png)

# Automated Tests

## Unit Tests

[NUnit](https://nunit.org/) is the framework used for unit-testing all the `Core` layer components : `Requests Handlers`, `Requests Validators`, `Services` and `Domain Objects`.

## Acceptance Tests

[Specflow](https://specflow.org) is the framework used to implement `Acceptance Tests`.

:warning: The application Use Cases are not all covered by `Acceptance Tests`, yet! The work is in progress :construction: :sunglasses:

# Contribution

For the moment, I will be the only contributor of the project. Nevertheless, you're welcome to report bugs or/and submit features by creating issues.
