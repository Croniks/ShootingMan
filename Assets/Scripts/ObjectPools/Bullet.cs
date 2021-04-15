using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    // должна быть ссылка на пулл объектов
    private Vector3 _shootDirection;
    private Transform _selfTransform;


    private void Awake()
    {
        _selfTransform = GetComponent<Transform>();
    }
    
    public void Setup(Vector3 shootDirection/*, параметр пулл объектов */)
    {
        _shootDirection = shootDirection;
    }
    
    private void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
