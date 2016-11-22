using Nancy;
using Wikibus.Common;

namespace Wikibus.Sources.Nancy
{
    /// <summary>
    /// Servers images of sources over HTTP
    /// </summary>
    public class SourceImagesModule : NancyModule
    {
        private const int SmallImageSize = 200;
        private readonly ISourceImagesRepository repository;
        private readonly IImageResizer resizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceImagesModule"/> class.
        /// </summary>
        public SourceImagesModule(ISourceImagesRepository repository, IImageResizer resizer)
        {
            this.ReturnNotFoundWhenModelIsNullOr(model => model.Length == 0);

            this.repository = repository;
            this.resizer = resizer;

            this.Get("/book/{id}/image", request => this.GetImage((int)request.id));
            this.Get("/brochure/{id}/image", request => this.GetImage((int)request.id));
            this.Get("/magazine/{mag}/issue/{issue}/image", request => this.GetImage((string)request.mag, (string)request.issue));
            this.Get("/book/{id}/image/small", request => this.GetImage((int)request.id, resize: true));
            this.Get("/brochure/{id}/image/small", request => this.GetImage((int)request.id, resize: true));
            this.Get("/magazine/{mag}/issue/{issue}/image/small", request => this.GetImage((string)request.mag, (string)request.issue, true));
        }

        private byte[] GetImage(string magazineName, string issueNumber, bool resize = false)
        {
            var imageBytes = this.repository.GetImageBytes(magazineName, issueNumber);
            if (resize)
            {
                return this.resizer.Resize(imageBytes, SmallImageSize);
            }

            return imageBytes;
        }

        private byte[] GetImage(int sourceId, bool resize = false)
        {
            var imageBytes = this.repository.GetImageBytes(sourceId);
            if (resize)
            {
                return this.resizer.Resize(imageBytes, SmallImageSize);
            }

            return imageBytes;
        }
    }
}
