using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerWeek7 : MonoBehaviour
{
    #region Serialized Variables

    #region Enums
    [Space, Header("Enums")]
    [Tooltip("Current Game State")]
    [SerializeField] private GameState _currGameState = GameState.Intro;
    private enum GameState { Intro, Game, End, Exit };
    #endregion

    #region UI
    [Space, Header("UI")]
    [SerializeField]
    [Tooltip("Menu Buttons")]
    private Button[] menuButtons = default;

    [SerializeField]
    [Tooltip("Fade Image Animation Component")]
    private Animator fadeBG = default;
    #endregion
    #endregion

    #region Private Variables
    [SerializeField] private bool _isGameRunning = default;
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

    void Update()
    {

    }
    #endregion

    #region My Functions

    /// <summary>
    /// Tied to Exit Button;
    /// Exits the game with Delay;
    /// </summary>
    public void OnClick_ExitGame() => StartCoroutine(QuitDelay());

    /// <summary>
    /// Disables all the Button interaction in the scene;
    /// </summary>
    public void DisableButtons()
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
    IEnumerator StartDelay()
    {
        fadeBG.Play("Fade_In");
        yield return new WaitForSeconds(0.5f);
        _currGameState = GameState.Game;
    }

    IEnumerator QuitDelay()
    {
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    IEnumerator EndDelay()
    {
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(0);
    }

    IEnumerator RestartDelay()
    {
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(Application.loadedLevel);
    }
    #endregion

    #region Events
    /// <summary>
    /// Subbed to event from DoggoController Script;
    /// Restarts the Game;
    /// </summary>
    void OnPlayerDeadEventReceived() => StartCoroutine(RestartDelay());

    void OnPlayerFinishEventReceived() => StartCoroutine(EndDelay());
    #endregion
}