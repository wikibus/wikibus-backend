namespace Wikibus.Common
{
    /// <summary>
    /// Handles resizing images
    /// </summary>
    public interface IImageResizer
    {
        /// <summary>
        /// Resizes the specified input image.
        /// </summary>
        /// <param name="input">The image contents.</param>
        /// <param name="maxSize">The maximum size.</param>
        byte[] Resize(byte[] input, int maxSize);
    }
}
