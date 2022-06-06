using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameManagerWeek10 : MonoBehaviour
{
    #region Serialized Variables

    #region UI
    [Space, Header("UI")]
    [SerializeField]
    [Tooltip("Pause Buttons")]
    private Button[] pauseButtons = default;

    [SerializeField]
    [Tooltip("Pause Panel")]
    private GameObject pausePanel;

    [SerializeField]
    [Tooltip("Fade Image Animation Component")]
    private Animator fadeBG = default;
    #endregion

    #endregion

    #region Private Variables
    #endregion

    #region Unity Callbacks
    void Start() => DisableCursor();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause(true);
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

    void ChangeScene(int index) => Application.LoadLevel(index);

    void TogglePause(bool isPaused)
    {
        if (isPaused)
        {
            EnableCursor();
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            DisableCursor();
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }

    }

    #region Buttons
    /// <summary>
    /// Function tied with Resume_Button Button;
    /// Resumes the Game;
    /// </summary>
    public void OnClick_Resume() => TogglePause(false);

    /// <summary>
    /// Function tied with Restart_Button Button;
    /// Restarts the game with a delay;
    /// </summary>
    public void OnClick_Restart() => StartCoroutine(RestartGameDelay());

    /// <summary>
    /// Button tied with Menu_Button;
    /// Goes to the Menu with a delay;
    /// </summary>
    public void OnClick_Menu() => StartCoroutine(MenuDelay());

    /// <summary>
    /// Function tied with Quit_Button Buttons;
    /// Quits the game with a delay;
    /// </summary>
    public void OnClick_Quit() => StartCoroutine(QuitGameDelay());

    /// <summary>
    /// Function tied with Restart_Button, Menu_Button and Quit_Button Buttons;
    /// Disables the buttons so the Player can't interact with them when the panel is fading out;
    /// </summary>
    public void OnClick_DisableButtons()
    {
        for (int i = 0; i < pauseButtons.Length; i++)
            pauseButtons[i].interactable = false;
    }
    #endregion

    #endregion

    #region Coroutines

    #region UI
    /// <summary>
    /// Starts game with a Delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator StartDelay()
    {
        fadeBG.Play("Fade_In");
        yield return new WaitForSeconds(0.5f);
    }

    /// <summary>
    /// Restarts the game with a Delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator RestartGameDelay()
    {
        TogglePause(false);
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        ChangeScene(Application.loadedLevel);
    }

    /// <summary>
    /// Goes to Menu with a Delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator MenuDelay()
    {
        TogglePause(false);
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        ChangeScene(0);
    }

    /// <summary>
    /// Quits with a Delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator QuitGameDelay()
    {
        TogglePause(false);
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    #endregion

    #endregion

    #region Events

    #endregion
}