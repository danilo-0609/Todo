namespace TodoManagement.Users.Contracts.Results;

public sealed record AuthenticationResult(string Login, string Token);