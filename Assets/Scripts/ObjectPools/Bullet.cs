using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Providers;


public class Bullet : MonoBehaviour
{
    private Transform _selfTransform;
    private GameObject _selfGameObject;
    private Vector3 _shootDirection;
    BulletsPool<Bullet> _pool;


    private void Awake()
    {
        _selfTransform = transform;
        _selfGameObject = gameObject;
    }
    
    public void Setup(Vector3 initialPosition, Vector3 shootDirection, BulletsPool<Bullet> pool)
    {
        _pool = pool;
        _selfTransform.position = initialPosition;
        _shootDirection = shootDirection;
        _selfGameObject.SetActive(true);
    }
    
    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Делаем что-нибудь при попадании

        _selfGameObject.SetActive(false);
        _pool.ReturnToPool(this);
    }
}