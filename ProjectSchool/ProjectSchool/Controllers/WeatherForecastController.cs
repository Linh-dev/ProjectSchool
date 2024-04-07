using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using ProjectSchool.Business;
using ProjectSchool.Models;
using System.Text;

namespace ProjectSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("TestGetNetTruyen")]
        public IEnumerable<DataModel> Get(string uri)
        {
            var data = new List<DataModel>();
            try
            {
                //var document = GetData.GetInstance().CreateDocs(uri);
                //var selectorListItem = "div.ModuleContent > div.items > div.row > div.item";
                //var threadItems = document.DocumentNode.QuerySelectorAll(selectorListItem).ToList();

                //foreach (var item in threadItems)
                //{
                //    var linkNode = item.QuerySelector("div.image > a");
                //    var link = linkNode.Attributes["href"].Value;
                //    var title = linkNode.Attributes["title"].Value;
                //    var d = new DataModel();
                //    d = GetData.GetInstance().GetDataBookNT(link);
                //    d.Description = link;
                //    d.Title = title;
                //    data.Add(d);
                //}

                ChromeOptions options = new ChromeOptions();
                options.AddArgument(@"user-data-dir=C:\Users\Admin\AppData\Local\Google\Chrome\User Data");
                //options.AddArgument(@"profile-dir=C:\Users\Admin\AppData\Local\Google\Chrome\User Data");
                //options.AddArguments("--headless");
                options.AddArguments("start-maximized"); // open Browser in maximized mode
                //options.AddArguments("disable-infobars"); // disabling infobars
                //options.AddArguments("--disable-extensions"); // disabling extensions
                //options.AddArguments("--disable-gpu"); // applicable to windows os only
                //options.AddArguments("--disable-dev-shm-usage"); // overcome limited resource problems
                //options.AddArguments("--no-sandbox"); // Bypass OS security model
                ChromeDriver chromeDriver = new ChromeDriver(options);
                //chromeDriver.Navigate();
                chromeDriver.Url = "https://www.youtube.com/";
                chromeDriver.Navigate();

                return data;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        [Route("TestGetDocTruyenOnline")]
        public IEnumerable<DataModel> TestGet(string uri)
        {
            var data = new List<DataModel>();
            try
            {
                var document = GetData.GetInstance().CreateDocs(uri);
                var selectorListItem = "div.bg-contain > div.mx-auto > div.pt-5 > div > div.px-3 > div.grid > div.col-span-1";
                var threadItems = document.DocumentNode.QuerySelectorAll(selectorListItem).ToList();

                foreach (var item in threadItems)
                {
                    var linkNode = item.QuerySelector("div > a.relative");
                    var link = GetData.LinkDTO + linkNode.Attributes["href"].Value;
                    var title = linkNode.Attributes["title"].Value;
                    var d = new DataModel();
                    d = GetData.GetInstance().GetDataBookDTO(link);
                    d.Description = link;
                    d.Title = title;
                    data.Add(d);
                }

                return data;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}