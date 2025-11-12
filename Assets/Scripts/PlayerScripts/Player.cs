using System.Collections.Generic;
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
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Light _playerLight;
    [SerializeField] private int _playerHealth = 10;
    private int _baseHealth = 10; // Store base health for respawn

    // Upgrade tracking: Key = upgrade type, Value = upgrade level
    private Dictionary<UpgradesEnum, int> _upgrades = new Dictionary<UpgradesEnum, int>();

    public Transform Transform { get => _transform; private set => _transform = value; }
    public PlayerMovement Movement { get => _movement; private set => _movement = value; }
    public Rigidbody Rigidbody { get => _rigidbody; private set => _rigidbody = value; }
    public Inventory Inventory { get => _inventory; private set => _inventory = value; }
    public Camera MainCamera { get => _mainCamera; private set => _mainCamera = value; }
    public GameObject CameraPivot { get => _cameraPivot; private set => _cameraPivot = value; }
    public InputActionAsset InputAction { get => _inputAction; private set => _inputAction = value; }
    public InputActionMap PlayerInputMap { get => _playerInputMap; private set => _playerInputMap = value; }
    public Dictionary<UpgradesEnum, int> Upgrades { get => _upgrades; private set => _upgrades = value; }
    public BoxCollider BoxCollider { get => _boxCollider; private set => _boxCollider = value; }
    public Light PlayerLight { get => _playerLight; private set => _playerLight = value; }
    public int PlayerHealth { get => _playerHealth; private set => _playerHealth = value; }

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
        if (!BoxCollider) BoxCollider = gameObject.GetComponent<BoxCollider>();

        if(!PlayerLight) PlayerLight = gameObject.GetComponentInChildren<Light>();

        // Store base health value
        _baseHealth = _playerHealth;
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

    public void LoadUpgradeData(Dictionary<UpgradesEnum, int> playerUpgrades)
    {
        _upgrades.Clear();
        if (playerUpgrades != null)
        {
            foreach (KeyValuePair<UpgradesEnum, int> kvp in playerUpgrades)
            {
                _upgrades[kvp.Key] = kvp.Value;
            }
        }
    }

    /// <summary>
    /// Resets health to base value. Call this before applying upgrades.
    /// </summary>
    public void ResetHealthToBase()
    {
        _playerHealth = _baseHealth;
        Debug.Log($"Player health reset to base: {_playerHealth}");
    }

    /// <summary>
    /// Called when [trigger enter] - handles enemy collision.
    /// </summary>
    /// <param name="other">The other collider.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Handle enemy collision
        if (other.CompareTag("Enemy"))
        {
            // Stop enemy from hunting player
            EnemyNavigation enemyNav = other.GetComponent<EnemyNavigation>();
            if (enemyNav != null)
            {
                enemyNav.StopHunting();
            }

            // Deal damage to player
            TakeDamage(10);

            // Trigger player caught event
            if (DayManager.Instance != null && PlayerHealth == 0)
            {
                // Reset health to base value
                PlayerHealth = _baseHealth;

                // Re-apply all upgrades (including Vigor) after respawn
                if (UpgradeManager.Instance != null)
                {
                    UpgradeManager.Instance.ApplyAllUpgrades(this);
                }

                DayManager.Instance.OnPlayerCaught();
            }
        }
    }

    public int TakeDamage(int damage)
    {
        if (damage >= PlayerHealth)
        {
            PlayerHealth = 0;
        }
        else
        {
            PlayerHealth -= damage;
        }
        return PlayerHealth;
    }

    public int increaseHealth(int amount)
    {
        if(amount >= 0)
        {
            PlayerHealth += amount;
        }
        return PlayerHealth;
    }
}
