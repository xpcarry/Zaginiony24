namespace Zaginiony24.Models
{
    public class ErrorCodes
    {
        public const string InternalServiceError = "InternalServiceError";
        public const string NoAccess = "NoAccess";
        public const string BadRequest = "BadRequest";

        public const string EmailIsIncorrect = "Adres e-mail jest niepoprawny";
        public const string InvalidUserName = "Nieprawidłowa nazwa użytkownika";
        public const string UserAlreadyExists = "Użytkownik już istnieje";
        public static string InvalidUsernameOrPassword = "Nieprawidłowa nazwa użytkowknika lub hasło";
        public static string OldPasswordIncorrect = "Old password is incorrect";
        public static string InvalidPasswordResetCode = "Invalid password reset code";
        public static string AccountTypeIsEmpty = "Account type is empty";
        public static string MissingApiKey = "Missing ApiKey";
        public static string InvalidToken = "Invalid Token";
        public static string UserWithThisEmailAlreadyExists = "Użytkownik z takim adresem e-mail nazwą już istnieje";
        public static string UserWithThisUsernameAlreadyExists = "Użytkownik z taką nazwą już istnieje";
        public static string PasswordDoNotMatch = "Hasła się nie zgadzają";
        public static string AccountLocked = "Konto jest obecnie zablokowane, skontaktuj się z administratorem systemu.";

        public static string CannotFindAnyNotice = "Nie można znaleźć żadnego ogłoszenia";
        public static string InvalidNoticeId = "Nieprawidłowe Id ogłoszenia";
    }
}
