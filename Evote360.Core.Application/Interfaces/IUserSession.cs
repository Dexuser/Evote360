using Evote360.Core.Application.ViewModels.User;
using Evote360.Core.Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Evote360.Core.Application.Interfaces
{
    public interface IUserSession
    {
        UserViewModel? GetUserSession();
        bool HasUser();
        bool IsAdmin();
        IActionResult RedirectToHomeByRole(UserViewModel userViewModel, Controller controller);
    }
}