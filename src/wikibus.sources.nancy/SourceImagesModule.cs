using Nancy;
using wikibus.common;

namespace wikibus.sources.nancy
{
    /// <summary>
    /// Servers images of sources over HTTP
    /// </summary>
    public class SourceImagesModule : NancyModule
    {
        private const int SmallImageSize = 200;
        private readonly ISourceImagesRepository _repository;
        private readonly IImageResizer _resizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceImagesModule"/> class.
        /// </summary>
        public SourceImagesModule(ISourceImagesRepository repository, IImageResizer resizer)
        {
            this.ReturnNotFoundWhenModelIsNullOr(model => model.Length == 0);

            _repository = repository;
            _resizer = resizer;

            Get["/book/{id}/image"] = request => GetImage((int)request.id);
            Get["/brochure/{id}/image"] = request => GetImage((int)request.id);
            Get["/magazine/{mag}/issue/{issue}/image"] = request => GetImage((string)request.mag, (string)request.issue);
            Get["/book/{id}/image/small"] = request => GetImage((int)request.id, resize: true);
            Get["/brochure/{id}/image/small"] = request => GetImage((int)request.id, resize: true);
            Get["/magazine/{mag}/issue/{issue}/image/small"] = request => GetImage((string)request.mag, (string)request.issue, true);
        }

        private byte[] GetImage(string magazineName, string issueNumber, bool resize = false)
        {
            var imageBytes = _repository.GetImageBytes(magazineName, issueNumber);
            if (resize)
            {
                return _resizer.Resize(imageBytes, SmallImageSize);
            }

            return imageBytes;
        }

        private byte[] GetImage(int sourceId, bool resize = false)
        {
            var imageBytes = _repository.GetImageBytes(sourceId);
            if (resize)
            {
                return _resizer.Resize(imageBytes, SmallImageSize);
            }

            return imageBytes;
        }
    }
}
