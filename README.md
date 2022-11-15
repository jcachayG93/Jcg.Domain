# Jcg.Domain

A library that abstracts some complexity from the domain layer, so the client code can better express intent.

It achieves this by decoupling the domain aggregate from the code that mutates its state and asserts invariant rules.

[More on the wiki](https://github.com/jcachayG93/jcg-domain-core/wiki)

## Dependencies
Net 7.0

## Features
### Domain Event Handlers Pipeline
- One stand-alone handler per domain event, that exists in its own class.

### Invariant Rule Handlers Pipeline
- One stand-alone handler per invariant rule that exists in its own class.

### Aggregate base class
- Has an aggregate version number increments each time a domain event is applied.
- Keeps track of all the domain events that were applied since the construction of the aggregate.
- Has a method to apply domain events.



## Installing the package

### NuGet:
> install-package jcg.domain

### DotNet
> dotnet add package jcg.domain


