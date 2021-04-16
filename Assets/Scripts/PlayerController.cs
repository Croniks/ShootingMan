using UnityEngine;
using UnityEngine.AI;
using Providers;


[RequireComponent(typeof(NavMeshAgent))] 
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    private Camera _mainCamera;
    private Transform _selfTransform;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private PlayerEvents _playerEvents;
    private bool _isShootingArea = false;
    private bool _isShooting = false;
    private Vector3 _bulletDestination;
    

    private void Awake()
    {
        _mainCamera = Camera.main;
        _selfTransform = GetComponent<Transform>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _playerEvents = ProvidersStorage.GetProvider<EventsProvider>().Get<PlayerEvents>();
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
                _navMeshAgent.isStopped = false;
                _isShooting = false;
            }
        }
        else if(Input.GetKey(KeyCode.Mouse1))
        {
            RaycastHit hit;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit, _layerMask) && _isShootingArea)
            {
                _bulletDestination = hit.point;
                Vector3 relative = _selfTransform.InverseTransformPoint(hit.point);
                float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
                _selfTransform.Rotate(0, angle, 0);
                
                _navMeshAgent.isStopped = true;
                _isShooting = true;
            }
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            _isShooting = false;
        }
        
        _animator.SetBool("IsWalking", _navMeshAgent.velocity.magnitude > 0.5f);
        _animator.SetBool("IsShooting", _isShooting && _isShootingArea);
    }
    
    private void OnCrossingShootingZone(bool isShootingArea)
    {
        _isShootingArea = isShootingArea;
        _animator.SetBool("IsShootingArea", _isShootingArea);
    }

    private void OnPlayerShoot()
    {
        //Debug.Log($"[{GetType()}.{nameof(OnPlayerShoot)}] Fire!");
        _playerEvents.PlayerShoot?.Invoke(_bulletDestination);
    }
}