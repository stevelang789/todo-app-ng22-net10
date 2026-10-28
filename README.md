# TodoApp

## Overview

- Entirely hand-coded
- Front end: Angular 22
- API: ASP.NET Web API .NET 10

## Highlights

### Front end

- `todo-detail.ts`
  - Usage of Signals Forms (introduced in Angular 21, stable in Angular 22)
  - Signals Forms validation using [validate()](https://angular.dev/guide/forms/signals/validation#using-validate):
    ```
    todoForm = form(this.todoModel, (schemaPath) => {
      required(schemaPath.description, { message: 'Description is required' });
      validate(schemaPath.description, ({ value }) => {
        if (value().trim().length === 0) {
          return {
            kind: 'required',
            message: 'Description is required'
          }
        }
        return null;
      });
    });
    ```
- `todo.service.ts`: Hides complexity by exposing only the `save()` method and by handling the different `POST` (create) and `PUT` (update) API endpoints internally.

### API

- RESTful API: GET, POST, PUT
- Different projects (assemblies) for the Web API and the core business logic
  - Separation of concerns
  - The API controller is very thin and does not contain any business logic
- xUnit.net v3 for the unit tests
  - `Moq.EntityFrameworkCore` to mock DbContext

## Caveats

- I would use RxJS instead of Promises for a real app. This is because RxJS supports coordination between multiple async sources.
- The unit tests should contain much more test cases, e.g., negative and positive tests.
- For a real app, I would add Playwright e2e tests to the front end using agentic AI. Boilerplate, repetitive code such as Playwright scripts is where AI really adds value. The developer would still need to come up with good test cases.
- I'm using Guids for IDs but I like to get the best of both worlds by using `long` as the primary key and a `Guid` as the public identifier.

## Getting it up and running

```
cd path\to\SteveLang.TodoApp\WebApi
dotnet dev-certs https --trust
dotnet run --launch-profile https
cd path\to\todo-app
npm install
npm start
```

Open http://localhost:4200 in a browser. Swagger: https://localhost:7294/swagger.
