using System.ComponentModel.DataAnnotations;

namespace demo1.model
{
    public class ProductImg
    {
        [Key]
        public int ImgId { get; set; }
        
        public int ProductId { get; set; }

        public byte[] Image { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public string FileContentType { get; set; }
    }
}
