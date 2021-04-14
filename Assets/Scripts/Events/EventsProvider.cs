using System.Collections.Generic;


namespace CustomEvents
{
    public interface IEventStorage { }
    public class EventsProvider
    {
        private static Dictionary<System.Type, IEventStorage> _eventsStorages = new Dictionary<System.Type, IEventStorage>();
        
        public static T Get<T>() where T : IEventStorage, new()
        {
            System.Type requestedType = typeof(T);

            if (_eventsStorages.ContainsKey(requestedType))
            {
                return (T)_eventsStorages[requestedType];
            }
            else
            {
                T eventStorage = new T();
                _eventsStorages.Add(requestedType, eventStorage);

                return eventStorage;
            }
        }
    }
}