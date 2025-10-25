using System.Net.Security;
using UnityEngine;
using UnityEngine.InputSystem;





public class Player : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _cameraPivot;
    [SerializeField] private InputActionAsset _inputAction;
    [SerializeField] private InputActionMap _playerInputMap;
    

    public Transform Transform { get => _transform; private set => _transform = value; }
    public PlayerMovement Movement { get => _movement; private set => _movement = value; }
    public Rigidbody Rigidbody { get => _rigidbody; private set => _rigidbody = value; }
    public Inventory Inventory { get => _inventory; private set => _inventory = value; }
    public Camera MainCamera { get => _mainCamera; private set => _mainCamera = value; }
    public GameObject CameraPivot { get => _cameraPivot; private set => _cameraPivot = value; }
    public InputActionAsset InputAction { get => _inputAction; private set => _inputAction = value; }
    public InputActionMap PlayerInputMap { get => _playerInputMap; private set => _playerInputMap = value; } 


    private void Awake()
    {
        if (!Transform) Transform = gameObject.transform;
        if (!Movement) Movement = gameObject.GetComponent<PlayerMovement>();
        if (!Rigidbody) Rigidbody = gameObject.GetComponent<Rigidbody>();
        if (!Inventory) Inventory = gameObject.GetComponentInChildren<Inventory>();
        if (!MainCamera) MainCamera = gameObject.GetComponentInChildren<Camera>();
        if (!CameraPivot) CameraPivot = GameObject.FindGameObjectWithTag("pivot");
        if(InputAction == null)
        {
            Debug.LogError("Assign [InputAction] in Player.cs on Player game object.");
        }
        else
        {
            PlayerInputMap = InputAction.FindActionMap("Player");
        }
        
    }

    public bool GetInteractPressed()
    {

        
        if (InputAction.FindAction("Interact").ReadValue<float>() == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
