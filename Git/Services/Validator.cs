using Git.Data;
using Git.ViewModels.Commits;
using Git.ViewModels.Repositories;
using Git.ViewModels.Users;
using System.Collections.Generic;

namespace CarShop.Services
{
    using static DataConstants;
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCommit(CommitCreationFormModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < CommitMinLength)
            {
                errors.Add("The description length must be 5 or above chars");
            }

            return errors;
        }


        public ICollection<string> ValidateRepository(RepositoryCreationFormModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < RepoMinLength || model.Name.Length > RepoMaxLength)
            {
                errors.Add($"Repo name must be between {RepoMinLength} and {RepoMinLength} chars long");
            }

            if (model.RepositoryType != Private && model.RepositoryType != Public)
            {
                errors.Add($"Repo type invalid");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(UserRegisterViewModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UsernameMinLength || model.Username.Length > UsernameMaxLength)
            {
                errors.Add($"Username length must be between {UsernameMinLength} and {UsernameMaxLength}");
            }

            if (model.Password.Length < PasswordMinLength || model.Password.Length > PasswordMaxLength)
            {
                errors.Add($"Password must be between {PasswordMinLength} and {PasswordMaxLength}");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Passwords don't match");
            }

            return errors;
        }
    }
}
