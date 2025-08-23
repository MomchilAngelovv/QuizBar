namespace QuizBar.Shared;

public static class ValidationErrors
{
    public static string NotFound(string entity) => $"{entity} not found";
    public static string AlreadyExists(string entity) => $"{entity} already exists";
    public static string FailedToCreateUser() => $"There was a problem while trying to create user";
    public static string FailedToAssignRole() => $"There was a problem while trying to asign role to a user";
}