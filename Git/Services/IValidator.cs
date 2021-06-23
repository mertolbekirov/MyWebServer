using Git.ViewModels.Users;
using System.Collections.Generic;

namespace CarShop.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(UserRegisterViewModel model);
    }
}
