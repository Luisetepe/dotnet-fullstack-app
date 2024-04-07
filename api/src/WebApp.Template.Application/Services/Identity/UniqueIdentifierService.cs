using TSID.Creator.NET;

namespace WebApp.Template.Application.Services.Identity;

public class UniqueIdentifierService : IUniqueIdentifierService
{
    public string Create()
    {
        return TsidCreator.GetTsid().ToString();
    }

    public string ConvertToString(long id)
    {
        return Tsid.From(id).ToString();
    }

    public long ConvertToNumber(string id)
    {
        return Tsid.From(id).ToLong();
    }
}
