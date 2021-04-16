using System;


namespace Providers
{
    class PlayerEvents : IProviderContent
    {
        public Action<bool> IsInShootZone;
        public Action PlayerShoot;
    }
}