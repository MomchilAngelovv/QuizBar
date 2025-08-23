using System.Text;

namespace QuizBar.Shared;

public static class ValidationErrors
{
    public static string ErrorMessage(Entity entity, ValidationError validationError)
    {
        var sb = new StringBuilder();

        switch (entity)
        {
            case Entity.None:
                break;
            case Entity.User:
                sb.Append("User");
                break;
            case Entity.Role:
                sb.Append("Role");
                break;
            case Entity.Quiz:
                sb.Append("Quiz");
                break;
            default:
                break;
        }

        sb.Append(" ");

        switch (validationError)
        {
            case ValidationError.None:
                break;
            case ValidationError.AlreadyExists:
                sb.Append("already exists");
                break;
            case ValidationError.NotFound:
                sb.Append("not found");
                break;
            case ValidationError.FailedToCreate:
                sb.Append("failed to create");
                break;
            default:
                break;
        }

        return sb.ToString();
    }
}

public enum Entity
{
    None = 0,
    User = 1,
    Role = 2,
    Quiz = 3
}

public enum ValidationError
{
    None = 0,
    AlreadyExists = 1,
    NotFound = 2,
    FailedToCreate = 3
}