using BusinessModels;
using UseCases.Exceptions;

namespace UseCases.Experts;

public class EditExpertUseCase
{
    private ExpertsStorage _storage { get; set; }

    public EditExpertUseCase(ExpertsStorage storage)
    {
        _storage = storage;
    }
    public void Execute(EditExpertRequest request)
    {
        if(request.Id == null)
            throw new InvalidEditRequestException();
        if(AllFieldAreEmpty(request))
            throw new InvalidEditRequestException();
        if(!_storage.Exists(request.Id))
            throw new ExpertNotFoundException();
        _storage.EditExpert(request);
    }
    private bool AllFieldAreEmpty(EditExpertRequest request)
    {
        return request.FirstName == null &&
               request.LastName == null &&
               request.Technology == null &&
               request.Description == null &&
               request.Role == null;
    }
}
