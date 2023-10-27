namespace TodoManagement.Todos.Domain.Common.Primitives
{
    public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
        where TId : AggregateRootID<TIdType>
    {
        public new AggregateRootID<TIdType> Id { get; protected set; }

        protected AggregateRoot(TId id)
        {
            Id = id;
        }

        protected AggregateRoot()
        {
        }
    }
}
