namespace ProjectSchool.Models
{
    [Serializable]
    public class DataModel : BaseModelInfo
    {
        public string Name { get; set; }
        public string OtherName { get; set; }
        /// <summary>
        /// tac gia
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        /// tinh trang
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// the loai
        /// </summary>
        public List<string> Genres { get; set; }
        /// <summary>
        /// so tap
        /// </summary>
        public int NumberOfEpisodes { get; set; }
        //anh nem
        public string ThumbnailImageUrl { get; set; }
        public string ThumbnailBase64Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> chapterModelIdList { get; set; }
    }
}
