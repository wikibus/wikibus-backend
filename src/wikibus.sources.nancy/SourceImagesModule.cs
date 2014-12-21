using Nancy;

namespace wikibus.sources.nancy
{
    /// <summary>
    /// Servers images of sources over HTTP
    /// </summary>
    public class SourceImagesModule : NancyModule
    {
        private readonly ISourceImagesRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceImagesModule"/> class.
        /// </summary>
        public SourceImagesModule(ISourceImagesRepository repository)
        {
            this.ReturnNotFoundWhenModelIsNullOr(model => model.Length == 0);

            _repository = repository;

            Get["/book/{id}/image/large"] = request => GetImage((int)request.id);
            Get["/brochure/{id}/image/large"] = request => GetImage((int)request.id);
            Get["/magazine/{mag}/issue/{issue}/image/large"] = request => GetImage((string)request.mag, (string)request.issue);
            Get["/book/{id}/image/small"] = request => GetImage((int)request.id).Resized();
            Get["/brochure/{id}/image/small"] = request => GetImage((int)request.id).Resized();
            Get["/magazine/{mag}/issue/{issue}/image/small"] = request => GetImage((string)request.mag, (string)request.issue).Resized();
        }

        private byte[] GetImage(string magazineName, string issueNumber)
        {
            return _repository.GetImageBytes(magazineName, issueNumber);
        }

        private byte[] GetImage(int sourceId)
        {
            return _repository.GetImageBytes(sourceId);
        }
    }
}
