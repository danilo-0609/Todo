using ErrorOr;
using System.Net.Mail;
using TodoManagement.Users.Domain.Common.DomainErrors;

namespace TodoManagement.Users.Domain.Common.ValueObjects;

public sealed record Email 
{
    public string Value { get; private set; }

    public static ErrorOr<Email> Create(string value)
    {
        bool isValid = ValidateEmail(value);

        if (!isValid)
        {
            return Errors.Users.InvalidCredentials;
        }

        return new Email(value);
    }

    private static bool ValidateEmail(string emailAddress)
    {
        try
        {
            MailAddress mailAddress = new(emailAddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    private Email(string value)
    {
        Value = value;
    }
}
