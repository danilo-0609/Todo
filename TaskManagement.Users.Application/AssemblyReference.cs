using System.Reflection;

namespace TodoManagement.Users.Application;

internal sealed class AssemblyReference
{
    internal static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
