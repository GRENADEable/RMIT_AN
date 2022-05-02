using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerWeek8 : MonoBehaviour
{
    #region Serialized Variables

    #region UI
    [Space, Header("UI")]
    [SerializeField]
    [Tooltip("Menu Buttons")]
    private Button[] menuButtons = default;

    [SerializeField]
    [Tooltip("Fade Image Animation Component")]
    private Animator fadeBG = default;

    [SerializeField]
    [Tooltip("Scene Numbner")]
    private int sceneNo = default;
    #endregion

    #endregion

    #region Private Variables
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
        StartCoroutine(StartDelay());
        DisableCursor();
    }
    #endregion

    #region My Functions
    void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void EnableCursor()
    {
        Cursor.visible = transform;
        Cursor.lockState = CursorLockMode.None;
    }
    #endregion

    #region Coroutines

    #region UI
    IEnumerator StartDelay()
    {
        fadeBG.Play("Fade_In");
        yield return new WaitForSeconds(0.5f);
    }
    #endregion

    #endregion
}