using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteraction : MonoBehaviour
{
    #region Serialized Variables
    //[SerializeField]
    //[Tooltip("")]

    #region Events
    public delegate void SendEvents();
    /// <summary>
    /// Event sent from BedInteraction to GameManagerWeek8 Script;
    /// Chooses the next bed to light up.
    /// </summary>
    public static event SendEvents OnBedInteract;
    #endregion

    #endregion

    #region Private Variables
    private Light _partyBedLight = default;
    private const string _defaultLayer = "Default";
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

    void Start() => _partyBedLight = GetComponentInChildren<Light>();

    void Update()
    {

    }
    #endregion

    #region My Functions
    /// <summary>
    /// Turns off the bed light and turns on the next one;
    /// </summary>
    public void InteractBed()
    {
        gameObject.layer = LayerMask.NameToLayer(_defaultLayer);
        _partyBedLight.enabled = false;
        OnBedInteract?.Invoke();
    }
    #endregion

    #region Coroutines

    #endregion

    #region Events

    #endregion
}