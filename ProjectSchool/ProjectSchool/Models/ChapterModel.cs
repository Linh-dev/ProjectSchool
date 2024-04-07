namespace ProjectSchool.Models
{
    public class ChapterModel : BaseModelInfo
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int Index { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public List<string> Base64Images { get; set; }
    }
}
