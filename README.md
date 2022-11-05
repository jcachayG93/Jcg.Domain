# JCG-DOMAIN-CORE

Abstracts complexity from a domain layer by decoupling the DomainEventHandlers and
InvariantRuleHandlers into sepparate classes that are assembled into pipelines.

## Dependencies
Net 6.0

## Getting Started
### Create Domain Events
```
public record PetCreated(Guid AggregateId, string Name):ICreationalDomainEvent

public record PetUpdated(Guid AggregateId, string Name) : INonCreationalDomainEvent
```

### Create the Aggregate

```
 internal class Pet : AggregateRootBase
    {
        public Pet(Guid id, string name)
        {
            var ev = new PetCreated(id, name);

            Apply(ev);
        }
        
        public void UpdateName(string name)
        {
           var ev  new PetUpdated(Id, name);
           
           Appy(ev);
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = "";

      

        /// <inheritdoc />
        protected override void When(IDomainEvent domainEvent)
        {
            var pipeline = DomainEventHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<Pet>();

            pipeline.Handle(this, domainEvent);
        }

        /// <inheritdoc />
        protected override void AssertEntityStateIsValid()
        {
            var pipeline = InvariantRuleHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<Pet>();

            pipeline.Handle(this);
        }

        /// <inheritdoc />
        protected override Guid GetId()
        {
            return Id;
        }
    }
```

### Create the Domain Event Handlers
```
internal class PetCreatedHandler : DomainEventHandlerBase<Pet>
{
protected override bool PerformHandling(Pet aggregate,
        IDomainEvent domainEvent)
    {
        if (domainEvent is DomainEvents.PetCreated cev)
        {
            aggregate.Id = cev.AggregateId;
            aggregate.Name = cev.Name;

            return true;
        }

        return false;
    }
}

internal class PetUpdatedHandler : DomainEventHandlerBase<Pet>
{
protected override bool PerformHandling(Pet aggregate,
        IDomainEvent domainEvent)
    {
        if (domainEvent is DomainEvents.PetUpdated cev)
        {           
            aggregate.Name = cev.Name;

            return true;
        }

        return false;
    }
}

```

### Create Invariant Rule Handlers
```
internal class PetNameIsRequiredRule
        : InvariantRuleHandlerBase<Pet>
    {
        /// <inheritdoc />
        protected override void AssertEntityStateIsValid(Pet aggregate)
        {
            if (string.IsNullOrEmpty(aggregate.Name))
            {
                throw new Exception("Pet name cant be empty");
            }
        }
    }
```

### Run
This is an example with unit test with XUnit

```
public class PetTests
{
  public void Creates()
  {
    // Arrange
    var id = Guid.NewGuid();
    var name = "aaa";
    // Act
    var sut = new Pet(id, name);
    // Assert
    Assert.True(sut.Name == name);
    Assert.True(sut.Id == id);
  }
  public void Updates()
  {
   // Arrange
   var sut = new Pet(Guid.NewGuid(),"aaa");
   // Act
   sut.UpdateName("bbb");
   // Assert
   Assert.True(sut.Name == "bbb");
  }
  public void Rule_NameIsRequired
  {
  // Arrange
  // Act
  var act = new Action(()=>
   {
    new Pet(Guid.NewGuid(),"");
   }
   );
  // Assert
  Assert.Throws<Exception>(act);
  }
}
```
