using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Providers;


public class ShootingManager : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _bulletsCount;
    private PlayerEvents _playerEvents;
    private BulletsPool<Bullet> _bulletsPool;
    private Transform _shotsSourceTransform;


    private void Awake()
    {
        _playerEvents = ProvidersStorage.GetProvider<EventsProvider>().Get<PlayerEvents>();
        _bulletsPool = ProvidersStorage.GetProvider<ObjectPoolsProvider>().Get<BulletsPool<Bullet>>();
        _shotsSourceTransform = GetComponent<Transform>();
    }

    void Start()
    {
        _playerEvents.PlayerShoot += OnPlayerShoot;
        _bulletsPool.Setup(_bulletPrefab, _bulletsCount);
    }
    
    private void OnPlayerShoot(Vector3 bulletDestination)
    {
        Bullet bullet = _bulletsPool.GetFromPool();

        if(bullet != null)
        {
            bullet.Setup(_shotsSourceTransform.position, CalculateDirection(bullet, bulletDestination), _bulletsPool);
        }
    }

    private Vector3 CalculateDirection(Bullet bullet, Vector3 bulletDestination)
    {
        
        
        return default;
    }
}