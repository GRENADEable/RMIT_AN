using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoController : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("Vel Clamp")]
    private float magClamp = default;
    #endregion

    #region Private Variables
    private Rigidbody _rg = default;
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
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        _rg.AddForce(Vector3.right * horizontal, ForceMode.Impulse);
        _rg.velocity = Vector3.ClampMagnitude(_rg.velocity, magClamp);
    }
    #endregion

    #region My Functions

    #endregion

    #region Coroutines

    #endregion

    #region Events

    #endregion
}