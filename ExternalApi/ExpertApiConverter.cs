using BusinessModels;

namespace ExternalApi;

public interface ExpertApiConverter
{
    Expert Convert(string expertJson);
}
