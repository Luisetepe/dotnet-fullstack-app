namespace WebApp.Template.Application.Services.Identity;

public interface IUniqueIdentifierService
{
    public string Create();
    string ConvertToString(long id);
    long ConvertToNumber(string id);
}
