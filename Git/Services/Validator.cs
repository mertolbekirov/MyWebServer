using Git.Data;
using Git.ViewModels.Users;
using System.Collections.Generic;

namespace CarShop.Services
{
    using static DataConstants;
    public class Validator : IValidator
    {
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
