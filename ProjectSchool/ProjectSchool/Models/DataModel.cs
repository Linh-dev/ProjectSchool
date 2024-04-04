namespace ProjectSchool.Models
{
    public class DataModel
    {
        public string Name { get; set; }
        public string OtherName { get; set; }
        /// <summary>
        /// tac gia
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// tinh trang
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// the loai
        /// </summary>
        public string MovieGenre { get; set; }
        /// <summary>
        /// so tap
        /// </summary>
        public int NumberOfEpisodes { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ChapterModel> chapterModels { get; set; }
    }
}
