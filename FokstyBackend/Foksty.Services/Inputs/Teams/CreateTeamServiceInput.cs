namespace Foksty.Services.Inputs.Teams;

public class CreateTeamServiceInput : IServiceInput
{
    public required string Title { get; set; }

    //IServiceInput
    public required string CreatedBy { get; set; }
}