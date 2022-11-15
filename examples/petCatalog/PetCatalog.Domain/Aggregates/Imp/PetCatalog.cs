using System.Reflection;
using Jcg.Domain.Aggregates;
using Jcg.Domain.Aggregates.DomainEventHandlers;
using Jcg.Domain.Aggregates.DomainEvents;
using Jcg.Domain.Aggregates.InvarianRuleHandlers;
using PetCatalogApp.Domain.ValueObjects;

namespace PetCatalogApp.Domain.Aggregates.Imp
{
    internal class PetCatalog : AggregateRootBase, IPetCatalog
    {
        public PetCatalog(CatalogId id, string name)
        {
            var ev = new DomainEvents.PetCatalogCreated(id.Id, name);

            Apply(ev);
        }

        public CatalogId Id { get; set; } = null!;
        public string CatalogName { get; set; } = "";
        #region Pets

        public IReadOnlyCollection<IPet> Pets => this.APets;

        public List<Pet> APets { get; set; } = new();
        #endregion
        public void AddPet(PetId petId, string name)
        {
            var ev = new DomainEvents.PetAdded(Id.Id, petId.Id, name);

            Apply(ev);
        }


        #region AggregateCommon

        protected override void When(IDomainEvent domainEvent)
        {
            var pipeline = DomainEventHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<PetCatalog>();

            pipeline.Handle(this, domainEvent);
        }

        protected override void AssertEntityStateIsValid()
        {
            var pipeline = InvariantRuleHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<PetCatalog>();

            pipeline.Handle(this);
        }

        protected override Guid GetId()
        {
            return this.Id.Id;
        }

        #endregion

       
    }
}
