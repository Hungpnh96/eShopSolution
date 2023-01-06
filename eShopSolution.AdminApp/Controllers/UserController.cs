using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        public UserController (IUserApiClient userApiClient , IConfiguration configuration )
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex =1, int pageSize=10)
        {
            var session = HttpContext.Session.GetString("Token");
            var request = new GetUserPaggingRequest()
            {
                BearerToken = session,
                KeyWord = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data =await _userApiClient.GetUserPaggings(request);
            return View(data);
        }

       

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register( RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RegisterUser(request);
            //Nếu đúng return phân trang
            if (result)
                return RedirectToAction("Index");

            return View(request);
        }

        //[HttpPost]
        //public JsonResult Register(RegisterRequest register)
        //{
        //    //Thông báo 
        //    string message = "";

        //    //Nếu không bypass
        //    if (!ModelState.IsValid)
        //        return Json(ModelState.Values, new Newtonsoft.Json.JsonSerializerSettings());



        //    return Json(message, new Newtonsoft.Json.JsonSerializerSettings());
        //}

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "User");
        }

    }
}
