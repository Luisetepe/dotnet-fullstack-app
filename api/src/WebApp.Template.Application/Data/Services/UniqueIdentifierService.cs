using TSID.Creator.NET;

namespace WebApp.Template.Application.Data.Services;

public class UniqueIdentifierService : IUniqueIdentifierService
{
    public long Create()
    {
        return TsidCreator.GetTsid().ToLong();
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
