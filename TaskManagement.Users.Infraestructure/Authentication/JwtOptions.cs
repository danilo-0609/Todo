namespace TodoManagement.Users.Infrastructure.Authentication;

internal sealed class JwtOptions
{
    public string Issuer { get; init; } = "TodoManagementApp";

    public string Audience { get; init; } = "TodoManagementService";

    public string SecretKey { get; init; } = "SuperSecretKeyForTodoManagement";
}
