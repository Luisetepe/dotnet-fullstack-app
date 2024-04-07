namespace WebApp.Template.Application.Features.Identity.Commands.SignInUser;

public record SignInUserResponse
{
    public string SessionId { get; init; }
    public string UserId { get; init; }
    public string Email { get; init; }
    public string UserName { get; init; }
    public string Role { get; init; }
    public string RoleId { get; init; }
    public IReadOnlyCollection<string> AllowedRoutes { get; set; }
}
