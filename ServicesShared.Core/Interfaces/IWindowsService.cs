namespace ServicesShared.Core
{
    public interface IWindowsService
    {
        void OnStart();

        void OnStop();
    }
}
