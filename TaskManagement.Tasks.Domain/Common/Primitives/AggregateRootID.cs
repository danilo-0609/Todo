namespace TodoManagement.Todos.Domain.Common.Primitives
{
    public abstract class AggregateRootID<TId> : ValueObject
    {
        public abstract TId Value { get; protected set; }
    }
}
