using System.IO;
using ImageResizer;

namespace wikibus.sources.nancy
{
    /// <summary>
    /// Simplify image manipulation
    /// </summary>
    public static class ImageResizerExtensions
    {
        /// <summary>
        /// Resizes the specified image bytes.
        /// </summary>
        public static byte[] Resized(this byte[] imageBytes)
        {
            if (imageBytes.Length == 0)
            {
                return imageBytes;
            }

            using (var stream = new MemoryStream())
            {
                ImageBuilder.Current.Build(imageBytes, stream, new ResizeSettings(200, 200, FitMode.Max, "jpg"));

                return stream.GetBuffer();
            }
        }
    }
}
