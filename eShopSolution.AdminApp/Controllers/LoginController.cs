using eShopSolution.AdminApp.Services;
using eShopSolution.Utilities.Constants;
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
    public class LoginController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public LoginController(IUserApiClient userApiClient,
            IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            //Check validate 
            if (!ModelState.IsValid)
                return View();

            //Call function Get authen
            var result = await _userApiClient.Authenticate(request);
            //Thành công
            if (result.IsSuccessed)
            {
                //lấy token 
                var userPrincipal = this.ValidateToken(result.ResultObject);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    IsPersistent = false
                };

                HttpContext.Session.SetString(SystemConstants.AppSetting.DefaultLanguageId, _configuration[SystemConstants.AppSetting.DefaultLanguageId]);
                HttpContext.Session.SetString(SystemConstants.AppSetting.Token, result.ResultObject);
                await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            userPrincipal,
                            authProperties);
                //redirect về trang home admin
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //gán lỗi và show lên giao diện login
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }

        [HttpPost]
        public async Task<JsonResult> Register(RegisterRequest request)
        {
            string Message = "";

            ResponseData response;
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userApiClient.RegisterUser(request);
                    if (result.IsSuccessed)
                    {
                        Message = "Đăng kí tài khoản: " + request.UserName + " thành công!";
                        response = new ResponseData(false, null, Message);
                    }
                    else
                    {
                        response = new ResponseData(true, null, result.Message);
                    }
                   
                }
                else
                {
                    //Lấy lỗi từ validation
                    Message = string.Join("; ", ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage == "" ? x.Exception.Message : x.ErrorMessage));
                    response = new ResponseData(true, null, Message);
                }
                
                
            }
            catch(Exception ex)
            {
                response = new ResponseData(true, null, ex.Message.ToString());
            }
            return Json(response);

        }


        public class ResponseData
        {
            public bool IsError { get; set; }
            public object Result { get; set; }
            public string Message { get; set; }
            public ResponseData()
            {

            }
            public ResponseData(bool isError, object result, string message)
            {
                IsError = isError;
                Result = result;
                Message = message;
            }
        }
    }
}
