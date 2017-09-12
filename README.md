# Recess - An Opinionated API Architecture for ASP.NET Core

## Overview

This project is meant to serve as a working example for the talk, **An Opinionated, Maintainable REST Architecture for ASP.NET Core**. It's highly recommended that you either a) [watch the talk](http://rest.schneids.net) or b) start reading a ton of [Jimmy Bogard's](https://twitter.com/jbogard) blog posts. Go ahead, I'll wait.

## What is it?

This project is an opinion by Spencer Schneidenbach about how APIs can be architected (from a code perspective) using ASP.NET Core.

Let me be clear - when I say architecture, I mean how the code is structured. Definitely not a full system architecture!

It's meant to be turnkey, but only as an experiment. If you really want, you can clone it and use it as a template, but make sure you know what you're getting you and your team into.

## Manifesto

* Separation of concerns is a good thing.
* Every action is broken down into requests (aka commands) and handlers. Requests gives the handlers enough information to do their jobs. Simply put...
* ...CQRS is a good thing.
* Entities should never be used to represent data in APIs - everything should go through a dumb object, or DTO, that is nothing but data.
* Controllers should know where to go to process a request, not how to process the request. Always and forever.
* Functionality should be separated by domain, not by category. Notice no Controllers or Models folders - Jobs and Employees (our example objects) are in their own folders.

You will agree with some of my decisions. You will also disagree with some of my decisions. The main point of this is to provide my opinion so you can form your own!

## Libraries used

* **MediatR** - this in-memory process delegator is the core of the project - it knows where to take requests for processing.
* **Autofac** - my favorite dependency injection library. Wires all of the project's dependencies together in a convenient, easy-to-understand way.
* **AutoMapper** - everyone's favorite mapper library for .NET. Easily define mappings between classes. `ProjectTo` is our god.
* **FluentValidation** - a deceptively simple way of validating requests coming into MediatR. Better than attribute validation.
* **Entity Framework** - my favorite way to access data. You don't have to use it - anything that gives you an IQueryable is good enough.
* **AutoMapper.Attributes** - a personal project of mine. Allows you to define mappings between classes using attributes.

## Getting started

0. Make sure you have the .NET Core SDK installed.
1. Clone the repo.
2. CD into src, then OpinionatedApiExample.
3. Run `dotnet restore`.
4. Run `dotnet run`.

## Experimentation

When you run the app, right now it won't look like it does much. I'd start by firing up Postman or curl or whatever you like, and start making requests to:

`GET /api/Employees` 
`POST /api/Employees` 
`GET /api/Employees/1` 
`GET /api/Jobs` 
`POST /api/Jobs` 

## Brief tour

**This part is a stub. It needs to be greatly expanded.**

The main thing you should be aware of is the fact that everything is broken down into requests and handlers. **Requests** represent a thing you want to do. **Handlers** take requests and performs the corresponding action - either get some data, create an object, etc. **Validators** can be used to make sure that requests are valid before they're processed.

Everything is decoupled. Requests are separate classes from handlers are separate classes from validators. The `OpinionatedRestController` is kind of my version of scaffolding - a lot of the magic is taken care of for you for GETs, PUTs, and POSTs. Just override and enjoy. Because you can always refactor later, right?? Right. :)

**WARNING: the PUT method as implemented is pretty weak! Like, super weak. It could be used to write to properties you didn't intend for your users to write to - it's more demonstrative than anything. YOU HAVE BEEN WARNED!**

There are two implemented APIs that you can peruse - Jobs and Employees. Both are roughly the same, but Job has a dependency on Employee.

### Employees

Employees are pretty basic objects, but represent something with a common business rule - properties that you want to be able to write to but not necessarily read from. This is represented by our use of the `SocialSecurityNumber` property.

* **Employee.cs** - the entity that is used by Entity Framework to store and retrieve Employee records. Has the `SocialSecurityNumber` property.
* **EmployeeModel.cs** - the DTO we use in GET requests. Note its lack of the `SocialSecurityNumber` property - we don't want to be able to read this.
* **EmployeePostModel.cs** - the DTO we use in POST (create) requests. Note that it DOES have the `SocialSecurityNumber` property for when the object is created.
* **EmployeePostValidator.cs** - the class that validates the `EmployeePostModel`. Makes sure incoming Employees have at least a first and last name.
* **EmployeesController.cs** - routes the requests to handlers. Most of the magic happens through the `OpinionatedRestController`.

### Jobs

Jobs are pretty basic objects, but require an Employee object to be associated to them. Pretty common use case.

* **Job.cs** - the entity that is used by Entity Framework to store and retrieve Job records.
* **JobModel.cs** - the DTO we use in GET requests.
* **JobPostModel.cs** - the DTO we use in POST (create) requests. Note that it has an `ProjectManagerId` as a required parameter - this is where you pass in the ID of the Employee.
* **JobPostValidator.cs** - the class that validates the `JobPostModel`. Note that it checks to make sure the Employee ID you're passing in via `ProjectManagerId` points to an existing Employee.
* **EmployeesController.cs** - routes the requests to handlers.

## TODOs

* **Unit Tests**
* Documentation
* Comments in code so it's explictly clear how stuff is done

## License

MIT.

## Thank you

[Jimmy Bogard's](https://twitter.com/jbogard) blog and talks served as the biggest inspiration for this talk and this project. His libraries AutoMapper and MediatR stand at the core of this project.