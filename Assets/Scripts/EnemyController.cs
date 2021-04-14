using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private List<Transform> _destinationPoints;
    private int _currentDestinationIndex;

    private Camera _mainCamera;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Transform _selfTransform;


    private void Awake()
    {
        _mainCamera = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _selfTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        if(_destinationPoints.Count > 0)
        {
            _selfTransform.position = _destinationPoints[0].position;
            _currentDestinationIndex = 1;
        }
    }

    private void Update()
    {
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }       
    }

    private void GotoNextPoint()
    {
        if (_destinationPoints.Count > 1)
        {
            _navMeshAgent.SetDestination(_destinationPoints[_currentDestinationIndex].position);
            _currentDestinationIndex = (_currentDestinationIndex + 1) % _destinationPoints.Count;
        }
    }
}