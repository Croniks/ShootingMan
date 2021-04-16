using System.Collections.Generic;


namespace Providers
{
    public interface IProviderContent{}

    public abstract class Provider
    {
        private Dictionary<System.Type, IProviderContent> _content = new Dictionary<System.Type, IProviderContent>();
        
        public T Get<T>() where T : IProviderContent, new()
        {
            System.Type contentType = typeof(T);

            if (_content.ContainsKey(contentType))
            {
                return (T)_content[contentType];
            }
            else
            {
                T newContent = new T();
                _content.Add(contentType, newContent);
                
                return newContent;
            }
        }
    }
    
    public abstract class ProvidersStorage
    {
        private static Dictionary<System.Type, Provider> _providersStorage = new Dictionary<System.Type, Provider>();
        
        public static T GetProvider<T>() where T : Provider, new()
        {
            System.Type providerType = typeof(T);

            if (_providersStorage.ContainsKey(providerType))
            {
                return (T)_providersStorage[providerType];
            }
            else
            {
                T providerInstance = new T();
                _providersStorage.Add(providerType, providerInstance);
                
                return providerInstance;
            }
        }
        
        public static void AddProvider<T>() where T : Provider, new()
        {
            System.Type providerType = typeof(T);

            if (!_providersStorage.ContainsKey(providerType))
            {
                T providerInstance = new T();
                _providersStorage.Add(providerType, providerInstance);
            }
        }
    }
}