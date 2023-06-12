using Aramex.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;

namespace Aramex.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7161/api");
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

      
        //private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        public IActionResult Index(string ReturnUrl)
        {
            ViewData["returnurl"]=ReturnUrl;
           //return RedirectToAction("dashboard");
            return View();
        }

        //[Authorize]
        //public IActionResult dashboard()
        //{
        //    return View();
        //}
        [Authorize]
        public IActionResult ShipTrack()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public bool Test()
        {
            return true;
        }
        [HttpPost]
        public async Task<IActionResult> Index(VMUser model,string? ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                try                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");                    HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/User/login", stringContent).Result;                    if (response.IsSuccessStatusCode)                    {
                        VMUser user = new VMUser();
                        var content = response.Content.ReadAsStringAsync().Result;
                        user = JsonConvert.DeserializeObject<VMUser>(content);
                        var claims = new List<Claim>
                            {
                              new Claim(ClaimTypes.Email,user.Email)
                            };
                        var claimsIdentity = new ClaimsIdentity(claims, "Login");

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


                        if (ReturnUrl==null)
                          {
                            TempData["success"] = "Logged in done succesfully";                            return RedirectToAction("SelectionPage");
                           }
                        else
                        {
                            return Redirect(ReturnUrl);
                        }                                                                      }                    else
                    {

                        TempData["notFound"] = "User Not Found";
                        return RedirectToAction("Index");
                    }                                   }                               catch (Exception ex)                {                    ViewData["ErrorMessage"] = "Error: " + ex.Message;                    return View("Index");                }

            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Registration(VMRegister model)
        {
            if (ModelState.IsValid)
            {
                try                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");                    HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/User/AddUsers", stringContent).Result;                    if (response.IsSuccessStatusCode)                    {


                        TempData["success"] = "Registration Done Succesfully";
                        //TempData.Keep("success"); 
                        //TempData.Clear();
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["failure"] = "Try Again With New Email Id";
                        
                    
                        return RedirectToAction("Registration");
                    }

                }

                catch (Exception ex)                {                    ViewData["ErrorMessage"] = "Error: " + ex.Message;                    return View("Index");                }

            }
            return RedirectToAction("Registration");
        }

        public IActionResult SelectionPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}