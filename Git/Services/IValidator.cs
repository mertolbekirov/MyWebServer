using Git.ViewModels.Commits;
using Git.ViewModels.Repositories;
using Git.ViewModels.Users;
using System.Collections.Generic;

namespace CarShop.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(UserRegisterViewModel model);
        ICollection<string> ValidateRepository(RepositoryCreationFormModel model);
        public ICollection<string> ValidateCommit(CommitCreationFormModel model);
    }
}
