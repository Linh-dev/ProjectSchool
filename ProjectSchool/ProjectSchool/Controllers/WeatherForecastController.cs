using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using ProjectSchool.Business;
using ProjectSchool.Dao;
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
        public IEnumerable<ComicInfo> Get(string uri)
        {
            var data = new List<ComicInfo>();
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

                var a = new ChapterInfo();
                a.Name = "chap 1";
                a.Title = "Title 1";
                ChapterDao.GetInstance().Insert(a);
                return data;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        [Route("GetAllUpdate")]
        public IEnumerable<ComicInfo> GetAllUpdate(string uri)
        {
            var data = new List<ComicInfo>();
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

                    var comicInfo = GetData.GetInstance().GetDataBookDTO(link, title);
                    comicInfo.Description = link;
                    comicInfo.Title = title;
                    if (string.IsNullOrEmpty(comicInfo.IdStr))
                    {
                        ComicDao.GetInstance().Insert(comicInfo);
                    }
                    else
                    {
                        ComicDao.GetInstance().Replace(comicInfo);
                    }
                    data.Add(comicInfo);
                }

                return data;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetAllGenre")]
        public IEnumerable<ComicInfo> GetAllGenre(string uri)
        {
            var data = new List<ComicInfo>();
            try
            {
                var document = GetData.GetInstance().CreateDocs(uri);
                var selectorListItem = "body > div > div > div > div > div > div.grid > div.col-span-1";
                var threadItems = document.DocumentNode.QuerySelectorAll(selectorListItem).ToList();
                foreach (var item in threadItems)
                {
                    var linkNode = item.QuerySelector("div > a.relative");
                    var link = GetData.LinkDTO + linkNode.Attributes["href"].Value;
                    var title = linkNode.Attributes["title"].Value;
                    var comicInfo = GetData.GetInstance().GetDataBookDTO(link, title);
                    comicInfo.Description = link;
                    comicInfo.Title = title;
                    if (string.IsNullOrEmpty(comicInfo.IdStr))
                    {
                        ComicDao.GetInstance().Insert(comicInfo);
                    }
                    else
                    {
                        ComicDao.GetInstance().Replace(comicInfo);
                    }
                    data.Add(comicInfo);
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