using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Providers;


public class ShootingManager : MonoBehaviour
{
    private PlayerEvents _playerEvents;
    private Transform _shotsSourceTransform;


    private void Awake()
    {
        _playerEvents = ProvidersStorage.GetProvider<EventsProvider>().Get<PlayerEvents>();
        _shotsSourceTransform = GetComponent<Transform>();
    }

    void Start()
    {
        _playerEvents.PlayerShoot += OnPlayerShoot;
    }

    private void OnPlayerShoot()
    {
        
    }
}