using Evote360.Core.Application.Helpers;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.User;
using Evote360.Core.Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EVote360.Middleware
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel? userViewModel = _httpContextAccessor.HttpContext?
                .Session.Get<UserViewModel>("user");

            if (userViewModel == null)
            {
                return false;
            }

            return true;
        }

        public UserViewModel? GetUserSession()
        {
            UserViewModel? userViewModel = _httpContextAccessor.HttpContext?
                .Session.Get<UserViewModel>("user");

            if (userViewModel == null)
            {
                return null;
            }

            return userViewModel;
        }

        public bool IsAdmin()
        {
            UserViewModel? userViewModel = _httpContextAccessor.HttpContext?
                .Session.Get<UserViewModel>("user");

            if (userViewModel == null)
            {
                return false;
            }

            return userViewModel.Role == (int)UserRole.Administrator;
        }

        public bool IsAuthenticated(UserRole roleToCompare)
        {
            throw new NotImplementedException();
        }
        
        public IActionResult RedirectToHomeByRole(UserViewModel userViewModel, Controller controller)
        {
            switch (userViewModel.Role)
            {
                case (int)UserRole.Administrator:
                    return controller.RedirectToRoute(new {controller = "AdminHome", action = "Index"});
                case (int)UserRole.PoliticalLeader:
                    return controller.RedirectToRoute(new {controller = "PartyLeaderHome", action = "Index"});
                default:
                    return controller.RedirectToRoute(new {controller = "Login", action = "Index"});
            }
        }
    }
}
