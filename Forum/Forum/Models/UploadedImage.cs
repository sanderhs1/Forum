namespace Forum.Models
{
    public class UploadedImage
    {
        public int Id { get; set; }
        public byte[] imageData { get; set; }
        public string ContentType { get; set; }

    }
}
