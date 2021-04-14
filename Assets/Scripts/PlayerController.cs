using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CustomEvents;


[RequireComponent(typeof(NavMeshAgent))] 
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    private Camera _mainCamera;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private PlayerEvents _playerEvents;
    private bool _isShootingArea = false;
    
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _playerEvents = EventsProvider.Get<PlayerEvents>();
    }

    private void Start()
    {
        _playerEvents.IsInShootZone += OnCrossingShootingZone;
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if(Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit, _layerMask))
            {
                _navMeshAgent.SetDestination(hit.point);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Debug.Log($"[{GetType()}.{nameof(Update)}] isShootingArea: {_isShootingArea}");

            if(_isShootingArea)
            {
                _animator.SetTrigger("StartFiringTrigger");
            }
        }

        _animator.SetBool("IsWalking", _navMeshAgent.velocity.magnitude > 0.5f);
    }
    
    private void OnCrossingShootingZone(bool isShootingArea)
    {
        _isShootingArea = isShootingArea;
        _animator.SetBool("IsShootingArea", _isShootingArea);
    }
}