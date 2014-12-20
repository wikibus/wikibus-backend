namespace wikibus.sources
{
    /// <summary>
    /// Repository of source images
    /// </summary>
    public interface ISourceImagesRepository
    {
        /// <summary>
        /// Gets raw image from database
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        byte[] GetImageBytes(int sourceId);
    }
}
