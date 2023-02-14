using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWeb.Areas.Admin.Models;
using ShopWeb.Data.Entities.Identity;
using System.Threading.Tasks;

namespace ShopWeb.Areas.Admin.Components
{
    [Authorize]
    public class NavProfileViewComponent : ViewComponent
    {
        private UserManager<UserEntity> _userManager;
        public NavProfileViewComponent(UserManager<UserEntity> userManager)
        {
            _userManager= userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            NavProfileViewModel model = new NavProfileViewModel();
            model.FristName = user.FirstName;
            model.LastName = user.LastName;
            model.Image = string.IsNullOrEmpty(user.Image) ? "select.png": user.Image;
            return View("_MenuItem", model);
        }
    }
}
