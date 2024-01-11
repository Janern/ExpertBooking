using BusinessModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Storage;
using UseCases.Exceptions;
using UseCases.Experts;
using Xunit;

namespace Tests.UseCases.Experts;

public class EditExpertUseCaseTests
{
    private EditExpertUseCase _useCase;
    private ExpertsStorageSpy _storage;
    public EditExpertUseCaseTests()
    {
        _storage = new ExpertsStorageSpy(new Expert[]{});
        InitializeUseCase();
    }    

    private void InitializeUseCase()
    {
        _useCase = new EditExpertUseCase(_storage);
    }
    
    [Fact]
    public void GivenNoExistingExpertsWhenEditingShouldThrow()
    {
        EditExpertRequest request = new EditExpertRequest{
            Id = "NONEXISTING", Description = "DESCIRPTION"
        };
        Assert.Throws<ExpertNotFoundException>(() => _useCase.Execute(request));
    }

    [Fact]
    public void GivenNoIdWhenEditingShouldThrow()
    {
        EditExpertRequest request = new EditExpertRequest();
        Assert.Throws<InvalidEditRequestException>(() => _useCase.Execute(request));
    }

    [Fact]
    public void GivenExistingExpertsWhenEditingNonExistingExpertShouldThrow()
    {
        Expert[] existing = new Expert[]
        {
            new Expert
            {
                Id = "ID",
                Description = "desc",
                FirstName = "",
                LastName = "",
                Role = "",
                Technology = ""
            }
        };
        _storage = new ExpertsStorageSpy(existing);
        InitializeUseCase();
        EditExpertRequest request = new EditExpertRequest{Id="NONEXISTING", Description = "NEW DESCPRITOTITON"};

        Assert.Throws<ExpertNotFoundException>(() => _useCase.Execute(request));
    }

    [Fact]
    public void GivenExistingExpertsWhenEditingExpertShouldNotThrow()
    {
        string Id = "EXISTING";
        Expert[] existing = new Expert[]
        {
            new Expert
            {
                Id = Id,
                Description = "desc",
                FirstName = "",
                LastName = "",
                Role = "",
                Technology = ""
            }
        };
        _storage = new ExpertsStorageSpy(existing);
        InitializeUseCase();
        EditExpertRequest request = new EditExpertRequest{Id = Id, Description = "NEWDESCRIPTION"};

        _useCase.Execute(request);
    }
    
    [Fact]
    public void GivenExistingExpertWhenEditingNoFieldsShouldThrow()
    {
        string Id = "EXISTING";
        Expert[] existing = new Expert[]
        {
            new Expert
            {
                Id = Id,
                Description = "DESCRIPTION",
                FirstName = "FRANZ",
                LastName = "Ferdinand",
                Role = "Backend",
                Technology = ".NET"
            }
        };
        _storage = new ExpertsStorageSpy(existing);
        InitializeUseCase();
         EditExpertRequest request = new EditExpertRequest{Id = Id};
        
        Assert.Throws<InvalidEditRequestException>(() => _useCase.Execute(request));
    }

    [Fact]
    public void GivenExistingExpertWhenEditingFieldsShouldEdit()
    {
        string Id = "EXISTING";
        Expert[] existing = new Expert[]
        {
            new Expert
            {
                Id = Id,
                Description = "oldDEscripton",
            }
        };
        _storage = new ExpertsStorageSpy(existing);
        InitializeUseCase();
        EditExpertRequest request = new EditExpertRequest{Id = Id, Description = "NEWDESCRIPTION"};
        
        _useCase.Execute(request);
      
        Assert.True(_storage.ExpertWasUpdated);
    }

    [Fact]
    public void GivenExistingExpertWhenEditingDescriptionShouldEditDescription()
    {
        string Id = "EXISTING";
        string NewDescription = "NEW DESCRIPTION";
        Expert[] existing = new Expert[]
        {
            new Expert
            {
                Id = Id,
                Description = "OLD DESCRIPTION",
                FirstName = "FRANZ",
                LastName = "Ferdinand",
                Role = "Backend",
                Technology = ".NET"
            }
        };
        _storage = new ExpertsStorageSpy(existing);
        InitializeUseCase();
        EditExpertRequest request = new EditExpertRequest{Id = Id, Description = NewDescription};
        
        _useCase.Execute(request);
    
        var expert = _storage.GetExpert(Id);
        Assert.Equal(NewDescription, expert.Description);
    }
    
    [Fact]
    public void GivenExistingExpertWhenEditingFirstNameShouldEditFirstName()
    {
        string Id = "EXISTING";
        string NewFirstName = "NEW Firstname";
        Expert[] existing = new Expert[]
        {
            new Expert
            {
                Id = Id,
                Description = "VERY GOOD PROGRAMMER",
                FirstName = "OLD FIRSTNAME",
                LastName = "Ferdinand",
                Role = "Backend",
                Technology = ".NET"
            }
        };
        _storage = new ExpertsStorageSpy(existing);
        InitializeUseCase();
        EditExpertRequest request = new EditExpertRequest{Id = Id, FirstName = NewFirstName};
        
        _useCase.Execute(request);
    
        var expert = _storage.GetExpert(Id);
        Assert.Equal(NewFirstName, expert.FirstName);
    }

    [Fact]
    public void GivenExistingExpertWhenEditingFirstNameAndDescriptionShouldEditFirstNameAndDescription()
    {
        string Id = "EXISTING";
        string NewFirstName = "NEW Firstname";
        string NewDescription = "NEW DEscription";
        Expert[] existing = new Expert[]
        {
            new Expert
            {
                Id = Id,
                Description = "OLD DESCRIPTION",
                FirstName = "OLD FIRSTNAME",
                LastName = "Ferdinand",
                Role = "Backend",
                Technology = ".NET"
            }
        };
        _storage = new ExpertsStorageSpy(existing);
        InitializeUseCase();
        EditExpertRequest request = new EditExpertRequest{Id = Id, FirstName = NewFirstName, Description = NewDescription};
        
        _useCase.Execute(request);
    
        var expert = _storage.GetExpert(Id);
        Assert.Equal(NewDescription, expert.Description);
        Assert.Equal(NewFirstName, expert.FirstName);
    }

    [Fact]
    public void GivenExistingExpertWhenEditingLastNameShouldEditLastName(){}
    
    [Fact]
    public void GivenExistingExpertWhenEditingFirstNameShouldEditRole(){}
    
    [Fact]
    public void GivenExistingExpertWhenEditingFirstNameShouldEditTechnology(){}

    private class ExpertsStorageSpy : ExpertsStorage
    {
        public bool ExpertWasUpdated = false;
        private ExpertsStorageInMemoryImplementation _expertStorage;

        public ExpertsStorageSpy(Expert[] existingExperts)
        {
            _expertStorage = new ExpertsStorageInMemoryImplementation(existingExperts);
        }

        public void EditExpert(EditExpertRequest request)
        {
            ExpertWasUpdated = true;
            _expertStorage.EditExpert(request);
        }

        public bool Exists(string id)
        {
            return _expertStorage.Exists(id);
        }

        public Expert GetExpert(string id)
        {
            return _expertStorage.GetExpert(id);
        }

        public Expert[] GetExperts(string technologyFilter, string[] expertIds = null)
        {
            return _expertStorage.GetExperts(technologyFilter, expertIds);
        }
    }
}