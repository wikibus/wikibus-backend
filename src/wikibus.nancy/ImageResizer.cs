using System.IO;
using ImageResizer;
using wikibus.common;

namespace wikibus.nancy
{
    /// <summary>
    /// Uses ImageResizer to resize images
    /// </summary>
    internal class ImageResizer : IImageResizer
    {
        /// <summary>
        /// Resizes the specified image bytes.
        /// </summary>
        public byte[] Resize(byte[] imageBytes, int maxSize)
        {
            if (imageBytes.Length == 0)
            {
                return imageBytes;
            }

            using (var stream = new MemoryStream())
            {
                ImageBuilder.Current.Build(imageBytes, stream, new ResizeSettings(maxSize, maxSize, FitMode.Max, "jpg"));

                return stream.GetBuffer();
            }
        }
    }
}
