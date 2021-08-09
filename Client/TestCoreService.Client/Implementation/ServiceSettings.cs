using ServicesShared.Core;

namespace TestCoreService.Client
{
    internal class ServiceSettings : IServiceSettings<IHandlingParameters>
    {
        private readonly ConfigurationHelper helper;

        public ServiceSettings()
        {
            helper = new ConfigurationHelper();
        }

        #region Properties

        /// <inheritdoc cref="IServiceInformation" />
        private int ServiceId => helper.GetIntValue();

        /// <inheritdoc cref="IServiceInformation" />
        private string ServiceName => helper.GetStringValue();

        /// <inheritdoc cref="IServiceInformation" />
        private string ServiceDisplayName => helper.GetStringValue();

        /// <inheritdoc cref="IServiceInformation" />
        private string ServiceDescription => helper.GetStringValue();

        #endregion       

        #region IServiceSettings implementation

        /// <inheritdoc />
        public IServiceInformation GetServiceInformation()
            => new ServiceInformation(ServiceId, ServiceName, ServiceDisplayName, ServiceDescription);

        /// <inheritdoc />
        public IHandlingParameters GetHandlingParameters()
            => new HandlingParameters();
     

        #endregion
    }
}