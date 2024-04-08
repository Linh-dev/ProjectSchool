using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using ProjectSchool.Dao;
using ProjectSchool.Extensions;
using ProjectSchool.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Text;

namespace ProjectSchool.Business
{
    public class GetData
    {

        public static string NameSpaceNT = "E:\\Images\\net-truyen";
        public static string NameSpaceDTO = "E:\\Images\\doc-truyen-online";
        public static string LinkDTO = "https://doctruyenonline.vn";
        private static GetData _instance;
        public static GetData GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GetData();
            }
            return _instance;
        }
        /// <summary>
        /// net truyen
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public DataModel GetDataBookNT(string uri)
        {
            var data = new DataModel();
            var document = CreateDocs(uri);
            var nameElement = document.DocumentNode.QuerySelectorAll("#item-detail > h1.title-detail").FirstOrDefault();
            if (nameElement != null)
            {
                data.Name = nameElement.InnerText;
            }
            else
            {
                data.Name = "abc";
            }
            
            var thumbnail = document.DocumentNode.QuerySelectorAll("div.detail-info > div.row > div.col-image > img").FirstOrDefault();
            var imageUrl = thumbnail.Attributes["src"].Value;
            var fileName = data.Name.RemoveSign4VietnameseString().RemoveSpecialCharacters();
            data.ThumbnailImageUrl = SaveImageWithWebClient(imageUrl, fileName, NameSpaceNT);
            var fisrtContent1 = document.DocumentNode.QuerySelectorAll("div.detail-info > div.row > div.col-info > ul.list-info").ToList();
            return data;
        }
        /// <summary>
        /// doc truyen online
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public DataModel GetDataBookDTO(string uri, string title)
        {
            var data = new DataModel();
            //check truyen ton tai
            var checkdata = DataModelDao.GetInstance().GetByTitle(title);
            var document = CreateDocs(uri);
            var folerChapterName = "";
            ChromeDriver chromeDriver = new ChromeDriver();
            chromeDriver.Url = uri;
            chromeDriver.Navigate();
            if (checkdata != null)
            {
                data = checkdata;
            }
            else
            {
                #region header
                var headerQuery = "body > div > header.pb-5 > div > div";
                var header = document.DocumentNode.QuerySelectorAll(headerQuery);

                if (header != null)
                {
                    //header 1: thumnail, name
                    var thumnailQuery = "figure > img";
                    var thumnailElement = header.ElementAt(1).QuerySelectorAll(thumnailQuery).FirstOrDefault();
                    data.Name = thumnailElement.Attributes["alt"].Value;
                    var fileName = data.Name.RemoveSign4VietnameseString().RemoveSpecialCharacters();
                    var imageUrl = thumnailElement.Attributes["src"].Value;

                    data.ThumbnailImageUrl = SaveImageWithSelenium(imageUrl, fileName, NameSpaceDTO, ref chromeDriver);
                    folerChapterName = NameSpaceDTO + fileName;
                    //header 2: author, genre
                    //1.author
                    var authorQuery = "div > div > a";
                    var authorElement = header.ElementAt(2).QuerySelectorAll(authorQuery).FirstOrDefault();
                    data.AuthorName = authorElement.Attributes["title"].Value;
                    //2. genre
                    var genreQuery = "div > ul > li > a";
                    var genreElement = header.ElementAt(2).QuerySelectorAll(genreQuery);
                    data.Genres = new List<string>();
                    foreach (var item in genreElement)
                    {
                        var genre = item.Attributes["itemprop"].Value;
                        data.Genres.Add(genre);
                    }
                }
                #endregion

                DataModelDao.GetInstance().Insert(data);
            }


            #region chapter list
            var chapterListQuery = "body > div > div > div > div > ul.relative > li > a";
            var chapterListElement = document.DocumentNode.QuerySelectorAll(chapterListQuery);
            foreach(var item in chapterListElement)
            {
                var currentUri = GetData.LinkDTO + item.Attributes["href"].Value;
                var chapterName = item.QuerySelectorAll("span").FirstOrDefault().InnerText;
                var checkChapter = ChapterModelDao.GetInstance().GetByName(chapterName);
                if (checkChapter != null) continue;
                var chapter = GetChapterDTO(currentUri, folerChapterName, chapterName, chromeDriver);
                chapter.Name = chapterName;
                chapter.DataId = data._id;
                ChapterModelDao.GetInstance().Insert(chapter);
            }
            #endregion
            return data;
        }

        public ChapterModel GetChapterDTO(string uri, string folderName, string chapterName, ChromeDriver chromeDriver)
        {
            var data = new ChapterModel();
            //check chapter ton tai
            var document = CreateDocs(uri);
            var listImageQuery = "body > div > div.bg-black > div.mx-auto > img.mx-auto";
            var listImageElement = document.DocumentNode.QuerySelectorAll(listImageQuery);
            int i = 0;
            
            chromeDriver.Url = uri;
            chromeDriver.Navigate();
            if(data.Images == null)
            {
                data.Images = new List<string>();
            }
            foreach (var img in listImageElement)
            {
                var imageUrl = img.Attributes["src"].Value;
                var imageFullUrl = SaveImageWithSelenium(imageUrl, i.ToString(), folderName + "\\" + chapterName, ref chromeDriver, false);
                data.Images.Add(imageFullUrl);
                i++;
            }
            return data;
        }

        public HtmlDocument CreateDocs(string uri)
        {
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
            };
            //Load trang web, nạp html vào document
            HtmlDocument document = htmlWeb.Load(uri);
            return document;
        }

        public string SaveImageWithWebClient(string imageUrl, string filename, string nameSpace)
        {
            //duoi anh
            var imageUrlArr = imageUrl.Split('.');
            var duoi = imageUrlArr[imageUrlArr.Length - 1];
            //folder upload
            var fileUpload = nameSpace + filename;
            //full url
            filename = filename + "." + duoi;
            var fullUrl = Path.Combine(fileUpload, filename);
            // get and save
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(imageUrl);
            Bitmap bitmap; bitmap = new Bitmap(stream);

            if (bitmap != null)
            {
                //check folder exist
                if (!Directory.Exists(fileUpload))
                {
                    Directory.CreateDirectory(fileUpload);
                }
                //checkfile exist
                if(File.Exists(fullUrl))
                {
                    File.Delete(fullUrl);
                }
                bitmap.Save(fullUrl);
            }
            stream.Flush();
            stream.Close();
            client.Dispose();

            return fullUrl;
        }
        /// <summary>
        /// luu base64
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="filename"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public string SaveImageWithSelenium(string imageUrl, string filename, string nameSpace, ref ChromeDriver chromeDriver, bool isNormalImg = true)
        {
            //duoi anh
            var imageUrlArr = imageUrl.Split('.');
            var duoi = imageUrlArr[imageUrlArr.Length - 1];
            var fileUpload = "";
            //folder upload
            if (isNormalImg)
            {
                fileUpload = Path.Combine(nameSpace , filename);
            }
            else
            {
                fileUpload = nameSpace;
            }
            //full url
            filename = filename + "." + duoi;
            var fullUrl = Path.Combine(fileUpload, filename);
            // get and save
            //WebClient client = new WebClient();
            //Stream stream = client.OpenRead(imageUrl);
            //Bitmap bitmap; bitmap = new Bitmap(stream);

            //if (bitmap != null)
            //{
            //    //check folder exist
            //    if (!Directory.Exists(fileUpload))
            //    {
            //        Directory.CreateDirectory(fileUpload);
            //    }
            //    //checkfile exist
            //    if (File.Exists(fullUrl))
            //    {
            //        File.Delete(fullUrl);
            //    }
            //    bitmap.Save(fullUrl);
            //}
            //stream.Flush();
            //stream.Close();
            //client.Dispose();

            chromeDriver.Url = imageUrl;
            chromeDriver.Navigate();

            var base64string = chromeDriver.ExecuteScript(@"
                    var c = document.createElement('canvas');
                    var ctx = c.getContext('2d');
                    var img = document.getElementsByTagName('img')[0];
                    c.height=img.naturalHeight;
                    c.width=img.naturalWidth;
                    ctx.drawImage(img, 0, 0,img.naturalWidth, img.naturalHeight);
                    var base64String = c.toDataURL();
                    return base64String;
                    ") as string;
            chromeDriver.Navigate().Back();

            var base64 = base64string.Split(',').Last();
            using (var stream = new MemoryStream(Convert.FromBase64String(base64)))
            {
                using (var bitmap = new Bitmap(stream))
                {
                    //check folder exist
                    if (!Directory.Exists(fileUpload))
                    {
                        Directory.CreateDirectory(fileUpload);
                    }
                    //checkfile exist
                    if (File.Exists(fullUrl))
                    {
                        File.Delete(fullUrl);
                    }
                    bitmap.Save(fullUrl);
                }
            }

            return fullUrl;
        }
    }
}
