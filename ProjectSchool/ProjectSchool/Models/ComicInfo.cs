using MongoDB.Bson;

namespace ProjectSchool.Models
{
    [Serializable]
    public class ComicInfo : BaseModelInfo
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
        /// the loai - ten
        /// </summary>
        public List<string> Genres { get; set; }
        /// <summary>
        /// the loai - id
        /// </summary>
        public List<ObjectId> GenreId { get; set; }
        /// <summary>
        /// so tap
        /// </summary>
        public int NumberOfEpisodes { get; set; }
        //anh nem
        public string ThumbnailImageUrl { get; set; }
        public string ThumbnailBase64Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
