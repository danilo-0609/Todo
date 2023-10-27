using System.Reflection;

namespace TodoManagement.Todos.Application;

internal sealed class AssemblyReference
{
    internal static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
