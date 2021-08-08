using Blazored.LocalStorage;
using Blazored.SessionStorage;

namespace MyStream.Core
{
    public interface IStorageService
    {
        public ISyncLocalStorageService Local { get; }
        public ISyncSessionStorageService Session { get; }
    }

    public class StorageService : IStorageService
    {
        public ISyncLocalStorageService Local { get; }
        public ISyncSessionStorageService Session { get; }

        public StorageService(ISyncLocalStorageService Local, ISyncSessionStorageService Session)
        {
            this.Local = Local;
            this.Session = Session;
        }
    }
}