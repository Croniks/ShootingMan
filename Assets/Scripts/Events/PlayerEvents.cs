using System;
using UnityEngine;


namespace Providers
{
    class PlayerEvents : IProviderContent
    {
        public Action<bool> IsInShootZone;
        public Action<Vector3> PlayerShoot;
        public Action<Vector3> DoExplosion;
    }
}