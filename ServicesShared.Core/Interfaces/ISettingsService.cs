namespace ServicesShared.Core
{
    public interface IServiceSettings<out T>
        where T : IBaseHandlingParameters
    {
        /// <summary>
        /// Returns a service information.
        /// </summary>
        IServiceInformation GetServiceInformation();

        /// <summary>
        /// Returns parameters that allows to manage handling communication with DataService.
        /// </summary>
        T GetHandlingParameters();      
    }
}
