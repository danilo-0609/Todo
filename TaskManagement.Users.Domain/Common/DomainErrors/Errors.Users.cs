using ErrorOr;

namespace TodoManagement.Users.Domain.Common.DomainErrors;

public static class Errors
{
    public static class Users
    {
        public static Error UserLoginIsNotUnique(string message) =>
            Error.Validation("Users.Login", message);

        public static Error UserCreatedWhenRegistrationWasNotConfirmed(string message) =>
            Error.Validation("Users.RegistrationConfirmation", message);

        public static Error UserRegistrationConfirmedTwice(string message) =>
            Error.Validation("Users.RegistrationConfirmation", message);

        public static Error UserRegistrationConfirmedAfterExpired(string message) =>
            Error.Validation("Users.RegistrationConfirmation", message);

        public static Error UserRegistrationExpiredTwice(string message) =>
            Error.Validation("Users.RegistrationExpired", message);

        public static Error InvalidCredentials =>
            Error.Validation("Users.InvalidCredentials", "The credentials are not valid");

        public static Error UserNotFound =>
            Error.NotFound("Users.NotFound", "The user was not found");

        public static Error InvalidPassword =>
            Error.Validation("Users.InvalidPassword", "The password you have passed is invalid");

        public static Error DuplicatedEmail =>
            Error.Validation("Users.DuplicatedEmail", "The email cannot be duplicated");

        public static Error PasswordLength =>
            Error.Validation("Users.Password", "Password length must be shorter than 20 characters");
    
    }
}
