using BusinessModels;

namespace ExternalApi;

public interface ApiClient
{
    public string GetExpertJson(string externalId);
}
