using System;


namespace CustomEvents
{
    class PlayerEvents : IEventStorage
    {
        public Action<bool> IsInShootZone;
    }
}