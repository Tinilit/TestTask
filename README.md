# TestTask

Small example of my work.
demo - http://demo.platform.com.ua

## Project setup

- Choose you local mssql server and put it in appsettings.json
- Run 'Update-database' to create database
- Add few categories manualy with sql command
- Run backend in visual studio
- Run frontend 'npm i' / 'ng s'

## N-layered architecture

As simple as possible. 
- Repositories used for manipulating data.
- Services used for dto mappings and computing results of few repository requests.
- DtoBuilders - pack of mapping rules.

## Models

Domain used for DB entities.
Models (from api lib) - has two types: dtos and request/response models
- dtos used to display and edit entities
- request/response used for complex requests (with filters, etc) and may include few types of dtos and additional information.

## ORM

Entity Framework Core with Code first approach and attributes instead of fluent api. 

## Error handing

We have custom ErrorFilterAttribute and ThrowValidationExceptionIfNotValid extension method, that allows as to build and throw http errors to client side with validation or other errors.

## Backend deployment

Database connection strings are stored in appsettings.json and appsettings.Production.json files. You have to replace it with your own for production environment if you want to publish the app. 

## Frontend deployment

Api url stored in environment.ts and environment.prod.ts file. You have to build your app with --prod flag to use second configuration for production.

## Frontend Architecture

Angular Cli used to build frontend app. So you can use standart angular-cli commands to manipulate the project.
- "ng s" - start app
- "ng build" - build app in dev mode
- "ng build --prod" - build app in prod mode

more command here - https://angular.io/cli

Most controls are taken from angular material library which is standard in our days.

## Other

- Swagger configured, so you can go to {project url}/swagger/index.html to take a look on api documentation.
- Project configured to take strings from resourse files. Few strings added only for samle.
- Decided do not emit event in modal window to update or add new object to grid. Reloading all data instead. This may help to keep sync with other grid users.
