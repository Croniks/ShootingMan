using System.Collections.Generic;


namespace Providers
{
    public abstract class AbstractProvider<SuppliedСontentInterface>
    {
        private static Dictionary<System.Type, SuppliedСontentInterface> _storage = new Dictionary<System.Type, SuppliedСontentInterface>();

        public static SuppliedСontent Get<SuppliedСontent>() where SuppliedСontent : SuppliedСontentInterface, new()
        {
            System.Type requestedType = typeof(SuppliedСontent);

            if (_storage.ContainsKey(requestedType))
            {
                return (SuppliedСontent)_storage[requestedType];
            }
            else
            {
                SuppliedСontent newSuppliedСontent = new SuppliedСontent();
                _storage.Add(requestedType, newSuppliedСontent);

                return newSuppliedСontent;
            }
        }
    }
}