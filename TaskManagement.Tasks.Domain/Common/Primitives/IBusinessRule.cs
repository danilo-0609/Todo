namespace TodoManagement.Todos.Domain.Common.Primitives
{
    public interface IBusinessRules
    {
        string Message { get; }

        bool IsBroken();
    }
}
