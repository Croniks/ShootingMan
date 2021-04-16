using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Providers;


public class Bullet : MonoBehaviour
{
    private float _bulletSpeed = 20f;
    private Transform _selfTransform;
    private GameObject _selfGameObject;
    private Vector3 _shootDirection;
    BulletsPool<Bullet> _pool;
    private PlayerEvents _playerEvents;


    private void Awake()
    {
        _selfTransform = transform;
        _selfGameObject = gameObject;
        _playerEvents = ProvidersStorage.GetProvider<EventsProvider>().Get<PlayerEvents>();
    }
    
    public void Setup(Vector3 initialPosition, Vector3 shootDirection, BulletsPool<Bullet> pool)
    {
        _pool = pool;
        _selfTransform.position = initialPosition;
        _selfTransform.rotation = Quaternion.LookRotation(shootDirection);
        _selfTransform.rotation = Quaternion.FromToRotation(Vector3.up, _selfTransform.forward);
        _shootDirection = shootDirection;
        _selfGameObject.SetActive(true);
    }
    
    private void Update()
    {
        _selfTransform.position += _shootDirection * _bulletSpeed * Time.deltaTime;
    }
    
    private void OnTriggerEnter()
    {
        _selfGameObject.SetActive(false);
        _pool.ReturnToPool(this);
        _playerEvents.DoExplosion?.Invoke(_selfTransform.position);
    }
}