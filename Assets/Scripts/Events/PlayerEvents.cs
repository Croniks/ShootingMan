using System;


namespace Providers.Events
{
    class PlayerEvents : IEventStorage
    {
        public Action<bool> IsInShootZone;
        public Action PlayerShoot;
    }
}