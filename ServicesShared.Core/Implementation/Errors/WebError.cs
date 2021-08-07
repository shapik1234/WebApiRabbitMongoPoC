using System.Text;
using System.Text.Json;

namespace ServicesShared.Core
{
    public class WebError : IError
    {
        public int Status { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        /// <inheritdoc  />
        public override string ToString()
        {
            return
                new StringBuilder()
                    .AppendLineIf(Status != 0, $"Status: {Status}")
                    .AppendLineIf(!Title.IsNullOrEmpty(), $"Title: {Title}")
                    .AppendLineIf(!Description.IsNullOrEmpty(), $"Description: {Description}")
                    .ToString();
        }

        #region IErrorWrapper implementation

        /// <inheritdoc cref="IError" />
        public int? Code => Status;

        /// <inheritdoc cref="IError" />
        public string StatusCode => Status.ToString();

        /// <inheritdoc cref="IError" />
        string IError.Title => Title;

        /// <inheritdoc cref="IError" />
        string IError.Description => Description;

        #endregion
    }
}
