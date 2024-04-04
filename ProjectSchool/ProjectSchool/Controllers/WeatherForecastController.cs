using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<DataModel> Get(string uri)
        {
            var data = new List<DataModel>();
            try
            {
                var document = GetData.GetInstance().CreateDocs(uri);
                var selectorListItem = "div.ModuleContent > div.items > div.row > div.item";
                var threadItems = document.DocumentNode.QuerySelectorAll(selectorListItem).ToList();

                foreach (var item in threadItems)
                {
                    var linkNode = item.QuerySelector("div.image > a");
                    var link = linkNode.Attributes["href"].Value;
                    var title = linkNode.Attributes["title"].Value;
                    var d = new DataModel();
                    d = GetData.GetInstance().GetDataBook(link);
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
        [HttpGet]
        [Route("TestGet")]
        public IEnumerable<DataModel> TestGet(string uri)
        {
            var data = new List<DataModel>();
            try
            {
                IWebDriver drive = new ChromeDriver();
                drive.Url = uri;
                var source = drive.PageSource;
                var a = drive.Manage().Network;
                drive.Navigate();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}