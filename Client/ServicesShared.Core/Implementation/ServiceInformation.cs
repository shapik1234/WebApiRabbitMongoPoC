namespace ServicesShared.Core
{
    public class ServiceInformation : IServiceInformation
    {
        public ServiceInformation(int id, string name, string displayName, string description)
        {
            Id = id;
            Name = name;
            DisplayName = displayName;
            Description = description;
        }

        #region IServiceInformation implementation

        /// <inheritdoc cref="IServiceInformation" />
        public int Id { get; }

        /// <inheritdoc cref="IServiceInformation" />
        public string Name { get; }

        /// <inheritdoc cref="IServiceInformation" />
        public string DisplayName { get; }

        /// <inheritdoc cref="IServiceInformation" />
        public string Description { get; }

        #endregion
    }
}
