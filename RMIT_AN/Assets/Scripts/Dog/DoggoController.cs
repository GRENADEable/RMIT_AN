using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoController : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("Vel Clamp")]
    private float magClamp = default;

    [SerializeField]
    [Tooltip("Vel Clamp")]
    private float upwardForce = default;

    [SerializeField]
    [Tooltip("Vel Clamp")]
    private Animator dogTailController = default;

    #region Events
    public delegate void SendEvents();
    /// <summary>
    /// Event sent from DoggoController to GameManager;
    /// Restarts the Scene;
    /// </summary>
    public static event SendEvents OnPlayerDead;

    /// <summary>
    /// Event sent from DoggoController to GameManager;
    /// Ends game;
    /// </summary>
    public static event SendEvents OnPlayerFinish;
    #endregion

    #endregion

    #region Private Variables
    private Rigidbody _rg = default;
    private bool _isGameEnded = default;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    void OnDestroy()
    {

    }
    #endregion

    void Start()
    {
        _rg = GetComponentInChildren<Rigidbody>();
        dogTailController.Play("Weiner_Dog_Tail_Anim_2");
    }

    void Update()
    {
        if (_isGameEnded)
            _rg.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            _rg.AddForce(Vector3.right * horizontal, ForceMode.Impulse);
        }

        _rg.velocity = Vector3.ClampMagnitude(_rg.velocity, magClamp);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
            OnPlayerDead?.Invoke();

        if (other.CompareTag("Finish"))
        {
            OnPlayerFinish?.Invoke();
            _isGameEnded = true;
        }
    }
    #endregion

    #region Coroutines

    #endregion
}