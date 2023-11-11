# Recess - An Opinionated API Architecture for ASP.NET Core

## Overview

This project is meant to serve as a working example for the talk, **An Opinionated, Maintainable REST Architecture for ASP.NET Core**. It's highly recommended that you either a) [watch the talk](https://www.youtube.com/watch?v=CH9VEeV-zok) or b) start reading a ton of [Jimmy Bogard's](https://twitter.com/jbogard) blog posts. Go ahead, I'll wait.

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
* Functionality should be separated by domain, not by category. Notice no Controllers or Models folders - Jobs and JobPhases (our example objects) are in their own folders.

You will agree with some of my decisions. You will also disagree with some of my decisions. The main point of this is to provide my opinions so you can form your own!

## Libraries used

* **MediatR** - this in-memory process delegator is the core of the project - it knows where to take requests for processing.
* **AutoMapper** - everyone's favorite mapper library for .NET. Easily define mappings between classes. `ProjectTo` is our god.
* **FluentValidation** - a deceptively simple way of validating requests coming into MediatR. Better than attribute validation.
* **Entity Framework Core** - my favorite way to access data.
* **Autofac** - my favorite dependency injection library. Wires all of the project's dependencies together in a convenient, easy-to-understand way.

## Getting started

0. Make sure you have the .NET Core SDK installed.
1. Clone the repo.
2. CD into src, then AspNetCoreExample.Api.
3. Run `dotnet restore`.
4. Run `dotnet run`.
5. ALSO: CD into AspNetCoreExample.Tests.
6. Run `dotnet test` and watch all the nice tests run!

## Experimentation

When you run the app, it won't look like it does anything. I'd start by firing up Postman or curl or whatever you like, and start making requests to:

`GET /api/Jobs`  
`POST /api/Jobs`  

and see what happens.

## Brief tour

**This part is a stub. It needs to be greatly expanded.**

The main thing you should be aware of is the fact that everything is broken down into requests and handlers. **Requests** represent a thing you want to do. **Handlers** take requests and performs the corresponding action - either get some data or create an object, etc. **Validators** can be used to make sure that requests are valid before they're processed.

There are two implemented APIs that you can peruse - Jobs and Job Phases. Both are roughly the same, but Job Phase has a dependency on Job.

### Jobs

Jobs are pretty basic objects, but require an Employee object to be associated to them. Pretty common use case.

* **Job.cs** - the entity that is used by Entity Framework to store and retrieve Job records.
* **CreateJobRequest.cs** - the POCO we use in POST (create) requests.
* **CreateJobValidator.cs** - the class that validates the `CreateJobRequest`.
* **JobsController.cs** - routes the requests to handlers.

## TODOs

* Expanded documentation
* Comments in code so it's explictly clear how/why stuff is done
* Paging

## License

MIT.
