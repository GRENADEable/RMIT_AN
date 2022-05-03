using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    #region Serialized Variables
    [Space, Header("Key Raycast")]
    [Tooltip("Debug Ray for Editor")]
    [SerializeField]
    private bool isDebugging = default;

    [SerializeField]
    [Tooltip("Which key to press when Interacting with Doors")]
    private KeyCode doorKey = KeyCode.E;

    [SerializeField]
    [Tooltip("Which layer(s) is the door?")]
    private LayerMask doorLayer = default;

    [SerializeField]
    [Tooltip("Raycast distance from the player camera")]
    private float rayDistance = 2f;
    #endregion

    #region Private Variables
    private Camera _cam = default;
    [SerializeField] private bool _isInteractingDoor = default;
    private Ray _ray = default;
    private RaycastHit _hit = default;
    #endregion

    #region Unity Callbacks
    void Start() => _cam = Camera.main;

    void Update()
    {
        _ray = new Ray(_cam.transform.position, _cam.transform.forward);
        RaycastCheckDoor();
    }
    #endregion

    #region My Functions

    /// <summary>
    /// Check for Door;
    /// </summary>
    void RaycastCheckDoor()
    {
        _isInteractingDoor = Physics.Raycast(_ray, out _hit, rayDistance, doorLayer);

        if (isDebugging)
            Debug.DrawRay(_ray.origin, _ray.direction * rayDistance, _isInteractingDoor ? Color.red : Color.white);

        if (_isInteractingDoor)
        {
            if (Input.GetKeyDown(doorKey))
            {
                if (_hit.collider.GetComponentInParent<DoorTrigger>() != null)
                    _hit.collider.GetComponentInParent<DoorTrigger>().InteractDoor();
            }
        }
    }
    #endregion
}