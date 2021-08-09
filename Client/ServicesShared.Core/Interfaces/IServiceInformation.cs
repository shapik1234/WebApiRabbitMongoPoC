namespace ServicesShared.Core
{
    public interface IServiceInformation
    {
        /// <summary>
        /// Gets a service id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the name of the service as it is registered in the service control manager.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the name of the service as it should be displayed in the service control manager.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the description of the service that is displayed in the service control manager.
        /// </summary>
        string Description { get; }
    }
}
