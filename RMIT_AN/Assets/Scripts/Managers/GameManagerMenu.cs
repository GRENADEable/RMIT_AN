using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMenu : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("Fade Image Animation Component")]
    private Animator fadeBG = default;

    [SerializeField]
    [Tooltip("Buttons Components")]
    private Button[] menuButtons = default;

    [SerializeField]
    [Tooltip("Scene Numbner")]
    private int sceneNo = default;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        fadeBG.Play("Fade_In");
        EnableCursor();
    }
    #endregion

    #region My Functions
    /// <summary>
    /// Tied to button Start_Button;
    /// Starts the Game;
    /// </summary>
    public void OnClick_StartGame() => StartCoroutine(StartGameDelay());

    /// <summary>
    /// Tied to button Quit_Button;
    /// Quits the Game;
    /// </summary>
    public void OnClick_QuitGame() => StartCoroutine(QuitGameDelay());

    /// <summary>
    /// Disables all the button interaction in the scene;
    /// </summary>
    public void OnClick_DisableButtons()
    {
        for (int i = 0; i < menuButtons.Length; i++)
            menuButtons[i].interactable = false;
    }

    #region Cursor
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
    #endregion

    #region Coroutines
    /// <summary>
    /// Starts the game with a delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator StartGameDelay()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(sceneNo);
    }

    /// <summary>
    /// Quits the game with a delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator QuitGameDelay()
    {
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    #endregion
}