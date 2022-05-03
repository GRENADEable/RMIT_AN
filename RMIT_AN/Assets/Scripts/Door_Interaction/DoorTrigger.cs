using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("Use iD numbers above zero as zero is default number. This is used to distinguish which door it will send events to")]
    private int triggerID = default;
    public bool isAccessable = default;

    #region Events
    public delegate void SendEventsInt(int index);
    /// <summary>
    /// Event sent from DoorTrigger Script to DoorController Script;
    /// This just passes an int iD so that specific door with the same iD is opened;
    /// </summary>
    public static event SendEventsInt OnDoorTrigger;
    #endregion

    #endregion

    #region My Functions
    /// <summary>
    /// Function that interacts with the door;
    /// </summary>
    /// <param name="playerObj"> Needs a GameObject in order to distinguish which door to open; </param>
    public void InteractDoor()
    {
        if (isAccessable)
            OnDoorTrigger?.Invoke(triggerID);
    }
    #endregion
}