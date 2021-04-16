using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Providers;


public class ShootingZone : MonoBehaviour
{
    private PlayerEvents _playerEvents;


    private void Awake()
    {
        _playerEvents = ProvidersStorage.GetProvider<EventsProvider>().Get<PlayerEvents>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerEvents.IsInShootZone?.Invoke(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        _playerEvents.IsInShootZone?.Invoke(false);
    }
}