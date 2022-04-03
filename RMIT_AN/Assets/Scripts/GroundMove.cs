using UnityEngine;

public class GroundMove : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("Ground Move Speed")]
    private float groundSpeed = 1f;
    #endregion

    #region Unity Callbacks
    void Update() => transform.Translate(Vector3.back * groundSpeed * Time.deltaTime);
    #endregion
}