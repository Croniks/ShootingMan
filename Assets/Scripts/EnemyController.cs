using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Providers;
using System;


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
    private PlayerEvents _playerEvents;
    private Rigidbody _rigidBody;
    private Settings _settings;
    private float _explosionForce;


    private void Awake()
    {
        _mainCamera = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _selfTransform = GetComponent<Transform>();
        _rigidBody = GetComponent<Rigidbody>();
        _playerEvents = ProvidersStorage.GetProvider<EventsProvider>().Get<PlayerEvents>();
        _settings = Resources.Load<Settings>("Settings/Settings");
    }

    private void Start()
    {
        _explosionForce = _settings.bulletPower;
        _playerEvents.DoExplosion += OnDoExplosion;

        if (_destinationPoints.Count > 0)
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

    private void OnDoExplosion(Vector3 explosionPoint)
    {
        //Debug.Log($"[{GetType()}.{nameof(OnDoExplosion)}] explosion point: {_selfTransform.position}");

        if(Vector3.Distance(_selfTransform.position, explosionPoint) < 20f)
        {
            this.enabled = false;
            _navMeshAgent.enabled = false;
            _animator.enabled = false;

            _rigidBody.AddExplosionForce(_explosionForce, explosionPoint, 10f, 5f);
        }
    }

    private void OnDisable()
    {
        _playerEvents.DoExplosion -= OnDoExplosion;
    }
}