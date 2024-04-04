using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
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
        private static GetData _instance;
        public static GetData GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GetData();
            }
            return _instance;
        }
        public DataModel GetDataBook(string uri)
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
            data.ThumbnailUrl = SaveImage(imageUrl, fileName);
            var fisrtContent1 = document.DocumentNode.QuerySelectorAll("div.detail-info > div.row > div.col-info > ul.list-info").ToList();
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

        public string SaveImage(string imageUrl, string filename)
        {
            //duoi anh
            var imageUrlArr = imageUrl.Split('.');
            var duoi = imageUrlArr[imageUrlArr.Length - 1];
            //folder upload
            var fileUpload = "E:\\Images\\" + filename;
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
    }
}
