using UnityEngine;

public class FPSControllerBasic : MonoBehaviour
{
    #region Serialized Variables
    [Space, Header("Player Keys")]
    [SerializeField]
    [Tooltip("Which key to press when running")]
    private KeyCode runKey = KeyCode.LeftShift;

    #region Player Variables
    [Space, Header("Player Variables")]
    [SerializeField]
    [Tooltip("Walk speed of the player")]
    private float playerWalkSpeed = 3f;

    [SerializeField]
    [Tooltip("Run speed of the player")]
    private float playerRunSpeed = 6f;

    [SerializeField]
    [Tooltip("Gravity of the player when falling")]
    private float gravity = -9.81f;
    #endregion

    #region Ground Check
    [Space, Header("Ground Check")]
    [SerializeField]
    [Tooltip("Transform Component for checking the ground")]
    private Transform groundCheck = default;

    [SerializeField]
    [Tooltip("Spherecast radius for the ground")]
    private float groundDistance = 0.4f;

    [SerializeField]
    [Tooltip("Which layer(s) is used for the ground?")]
    private LayerMask groundMask = default;
    #endregion

    #region Events
    public delegate void SendEvents();
    /// <summary>
    /// Event sent from FPSControllerBasic to GameManager;
    /// Restarts the map with delay;
    /// </summary>
    public static event SendEvents OnPlayerDead;
    #endregion

    #endregion

    #region Private Variables
    [Header("Player Variables")]
    private CharacterController _charControl = default;
    private Vector3 _vel = default;
    private float _currSpeed = default;
    private bool _isGrounded = default;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _charControl = GetComponent<CharacterController>();
        _currSpeed = playerWalkSpeed;
    }

    void Update()
    {
        GroundCheck();
        PlayerCurrStance();
        PlayerMovement();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death_Box"))
            OnPlayerDead?.Invoke();
    }
    #endregion

    #region My Functions
    /// <summary>
    /// Ground check for gavity and jumping;
    /// </summary>
    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _vel.y < 0)
            _vel.y = -2f;

        _vel.y += gravity * Time.deltaTime;
        _charControl.Move(_vel * Time.deltaTime);
    }

    /// <summary>
    /// This is where the player movement takes place;
    /// </summary>
    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * horizontal + transform.forward * vertical).normalized;
        _charControl.Move(_currSpeed * Time.deltaTime * moveDirection);
    }

    /// <summary>
    /// Checks stance if the player is Running;
    /// </summary>
    void PlayerCurrStance()
    {
        if (Input.GetKeyDown(runKey))
            _currSpeed = playerRunSpeed;

        if (Input.GetKeyUp(runKey))
            _currSpeed = playerWalkSpeed;
    }
    #endregion
}