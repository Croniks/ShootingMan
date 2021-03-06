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
    private Settings _settings;
   
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _selfTransform = GetComponent<Transform>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _playerEvents = ProvidersStorage.GetProvider<EventsProvider>().Get<PlayerEvents>();
        _settings = Resources.Load<Settings>("Settings/Settings");  
    }
    
    private void Start()
    {
        _navMeshAgent.speed = _settings.playerSpeed;
        _animator.SetFloat("Speed", (float)_settings.fireSpeed);
        _playerEvents.IsInShootZone += OnCrossingShootingZone;
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if(Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _layerMask))
            { 
                _navMeshAgent.SetDestination(hit.point);
                _navMeshAgent.isStopped = false;
                _isShooting = false;
            }
        }
        else if(Input.GetKey(KeyCode.Mouse1))
        {
            RaycastHit hit;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _layerMask) && _isShootingArea)
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

        if (Input.GetKeyUp(KeyCode.F))
        {
            Debug.Log($"[{GetType()}.{nameof(Update)}] animator: {_animator != null}");
        }
    }
    
    private void OnCrossingShootingZone(bool isShootingArea)
    {
        _isShootingArea = isShootingArea;
        _animator.SetBool("IsShootingArea", _isShootingArea);
    }

    private void OnPlayerShoot()
    {
        _playerEvents.PlayerShoot?.Invoke(_bulletDestination);
    }

    private void OnDisable()
    {
        _playerEvents.IsInShootZone -= OnCrossingShootingZone;
    }
}