namespace WebApp.Template.Application.Data.Services;

public interface IUniqueIdentifierService
{
    public long Create();
    string ConvertToString(long id);
    long ConvertToNumber(string id);
}
