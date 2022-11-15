# Example application for jcg-domain-core

This sample application consists of a domain project and a test project.

The following are some notes about it.

## Encapsulation of the domain

This is an example of one of several ways to encapsulate the domain layer. This is the way I work when using this library.

See the following code for the aggregate in this project:

***IPetCatalog.cs***
```
 public interface IPetCatalog
    {
        CatalogId Id { get; }

        string CatalogName { get; }

        IReadOnlyCollection<IPet> Pets { get; }

        void AddPet(PetId petId, string name);

    }
```

***PetCatalog.cs***
```
  internal class PetCatalog : AggregateRootBase, IPetCatalog
    {
        public PetCatalog(CatalogId id, string name)
        {
            ...
        }
        public CatalogId Id { get; set; }
        public string CatalogName { get; set; } = "";
        #region Pets

        public IReadOnlyCollection<IPet> Pets => this.APets;

        public List<Pet> APets { get; set; } = new();
        #endregion
        public void AddPet(PetId petId, string name)
        {
            ...
        }

        ...
       
    }
```

### The interface is exposed to the other layers
The interface is public, and inmutable, forcing the clients to use aggregate methods to operate on the aggregate.

### The implementation is internal and mutable
The implementation (the class above) has **internal access** so it can only be accessed by the domain layer (and potentially other layers like Infrastructure by granting internal access)

It also has automatic properties:

```
public string CatalogName { get; set; } = "";
```

so the domain event handlers, and potentially the persistence layer can operate freely on it. This can also be achieved by providing methods for this purpose that are members of the implementation but not the interface. 

### Use factories
For the application layer to create the aggregate, you just need to create a factory service, not shown in this project:

For example:

```
public class PetCatalogFactoryService : IPetCatalogFactoryService
{
    public IPetCatalog Create(CatalogId id, string name)
    {
        return new PetCatalog(id, name);
    }
}
```

## Domain events


### Create domain events

***DomainEvents.cs***
```
  public static class DomainEvents
    {
        public record PetCatalogCreated(Guid AggregateId, string Name) : ICreationalDomainEvent;

        public record PetAdded(Guid AggregateId, Guid PetId, string PetName) : INonCreationalDomainEvent;
    }
```

Observe there are two types of domain events: ICreationalDomainEvent and INonCreationalDomainEvent

Both implement the **IDomainEvent** interface wich has a single AggregateId property.

The difference is:

> Applying a NonCreational domain event to an aggregate with an Id that does not match the event AggregateId value will throw an exception.

The Creational domain event skips this check.

### Add a domain event handler

***PetAddedHandler.cs***
```
internal class PetAddedHandler : DomainEventHandlerBase<PetCatalog>
{
    protected override bool PerformHandling(PetCatalog aggregate, IDomainEvent domainEvent)
    {
        if (domainEvent is DomainEvents.PetAdded cev)
        {
           // ... crete a pet and add it to the aggregate
                
           // You must return true so the library knows the event was handled.
            return true;
        }

        // return false if the domain event type did not match the type intended for this handler,
        // so the library can try the next handler in the pipeline
        return false;
    }
}
```

> If you apply a domain event for which a handler does not exists, an exception will be thrown.

### Find the domain events and build the handler pipeline

See this method inside the aggregate

***PetCatalog.cs***
```
protected override void When(IDomainEvent domainEvent)
        {
            var pipeline = DomainEventHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<PetCatalog>();

            pipeline.Handle(this, domainEvent);
        }
```

the following code
```
DomainEventHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<PetCatalog>();
```

**DomainEventHandlingPipelineProvider** is a singleton. 

The first time you call it (during the application lifetime), it scans the assembly for all domain event handlers for all existing aggregates,
and it uses these handlers to build a pipeline (Chain of Reposibility GOF Pattern).

The following code
```
.GetPipeline<PetCatalog>();
```

gets the pipeline from the provider.

The code
```
pipeline.Handle(this, domainEvent);
```

Uses the pipeline to update the aggregate state.

> Because the DomainEventHandligPipelineProvider is a singleton, the pipeline is assembled only once during the application lifetime.

### Apply a domain event in the aggregate
To apply a domain event, just create an instance of the event and use the Apply method as in the following example.

***PetCatalog.cs***
```
 internal class PetCatalog : AggregateRootBase, IPetCatalog
    {
       ...

        public void AddPet(PetId petId, string name)
        {
            var ev = new DomainEvents.PetAdded(Id.Id, petId.Id, name);

            Apply(ev);
        }


       ...

       
    }
```

**Avoid changing the aggregate state in any other way than applying domain events.

## Invariant Rule Handlers

> Handlers that assert that the aggregate fulfills all it's invariants. Each Handler implements one single rule. 

> You can have zero, one or as many handlers as needed.

### Add an Invariant rule handler

***PetNameIsRequiredRuleHandler.cs***
```
internal class PetNameIsRequiredRuleHandler : InvariantRuleHandlerBase<PetCatalog>
{
    protected override void AssertEntityStateIsValid(PetCatalog aggregate)
    {
        if (aggregate.APets.Any(p => string.IsNullOrWhiteSpace(p.Name)))
        {
            throw new PetNameIsBlankException();
        }
    }
}
```

### Find the Invariant Rule Handlers and build the handler pipeline

See this method inside the aggregate

***PetCatalog.cs***
```
 protected override void AssertEntityStateIsValid()
        {
            var pipeline = InvariantRuleHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<PetCatalog>();

            pipeline.Handle(this);
        }
```

Acts in similar fasion than the DomainEvent Handlers, but, for InvariantRuleHandlers


> Because the DomainEventHandligPipelineProvider is a singleton, the pipeline is assembled only once during the application lifetime.

## Other important AggregateRootBase methods

### Optimistic concurrency version

***AggregateRootBase.cs***
```
public long Version { get; protected set; }
```

This value increments each time a domain event is applied. You should set it when loading an aggregate from the database (that is why it has protected access). Set it to zero when the aggregate is constructed for the first time (unless is been loaded from a database)

### Changes

Use this method to get the Changes (all the domain events that were added since the aggregate was constructed)

***AggregateRootBase.cs***
```
public IDomainEvent[] Changes => _changes.ToArray();
```

A typical use for this method is to publish this domain events when saving the aggregate to the database.

Use the following method to reset the Changes list to an empty collection

***AggregateRootBase.cs***
```
public void ResetChanges();
```

A typical use case for this method is to reset the changes when the domain events were published or when loading the aggregate from the database.


