namespace ServicesShared.Core
{
    public interface IError
    {
        /// <summary>
        /// Gets a error code.
        /// </summary>
        int? Code { get; }

        /// <summary>
        /// Gets a status code.
        /// </summary>
        string StatusCode { get; }

        /// <summary>
        /// Gets a title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets a description.
        /// </summary>
        string Description { get; }
    }
}
