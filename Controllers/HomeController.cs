using app2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using app2.Models;

namespace app1.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        /*
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        */

        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Index(CircleViewModel model)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:5270/api/Circle/cv_dt?rr={model.rr}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<dynamic>(json);
                model.DienTich = data.dien_tich;
                model.ChuVi = data.chu_vi;
                model.DuongKinh = data.duong_kinh;
            }

            return View(model);
        }

        public IActionResult Index()
        {
            return View(new CircleViewModel());
        }

        public IActionResult Privacy()
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