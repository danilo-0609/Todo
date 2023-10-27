using ErrorOr;
using System.Security.Cryptography;
using System.Text;
using TodoManagement.Users.Domain.Common.DomainErrors;

namespace TodoManagement.Users.Domain.Users.ValueObjects;

public sealed record Password
{
    private const int MaximumLength = 20;

    public string PasswordValue { get; private set; } = string.Empty;

    public static ErrorOr<Password> Create(string value)
    {
        if (value.Length > MaximumLength)
        {
            return Errors.Users.PasswordLength;
        }

        string passwordHash = ToSha256(value);

        Password password = new Password(passwordHash);

        return password;
    }

    private Password(string value)
    {
        PasswordValue = value;
    }
    
    private static string ToSha256(string value)
    {
        using var sha256 = SHA256.Create();

        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));

        StringBuilder passwordBuilder = new StringBuilder();

        foreach (var b in bytes)
        {
            passwordBuilder.Append(b.ToString());
        }

        string password = passwordBuilder.ToString();

        return password;
    }
}
