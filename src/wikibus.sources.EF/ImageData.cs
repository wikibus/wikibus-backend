using System.ComponentModel.DataAnnotations;

namespace Wikibus.Sources.EF
{
    public class ImageData
    {
        [Key]
        public int Id { get; set; }

        public byte[] Image { get; set; }
    }
}