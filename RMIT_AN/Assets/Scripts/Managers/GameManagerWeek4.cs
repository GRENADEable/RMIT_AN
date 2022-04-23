using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameManagerWeek4 : MonoBehaviour
{
    #region Serialized Variables

    #region Enums
    [Space, Header("Enums")]
    [Tooltip("Current Game State")]
    [SerializeField] private GameState _currGameState = GameState.Menu;
    private enum GameState { Menu, Intro, Game, End, Exit };
    #endregion

    #region Player
    [Space, Header("Player")]
    [SerializeField]
    [Tooltip("Secondary Doggo GameObject")]
    private GameObject runningDogObj = default;

    [SerializeField]
    [Tooltip("Decrement Speed")]
    private float decrementSpeed = default;

    [SerializeField]
    [Tooltip("Player Root Anim Controller")]
    private Animator playerRootAnim = default;

    [SerializeField]
    [Tooltip("Player Tail Anim Controller")]
    private Animator playerTailAnim = default;

    [SerializeField]
    [Tooltip("Energy Slider")]
    private Slider energyBar = default;
    #endregion

    #region Level Transition
    [Space, Header("Level Transition")]
    [SerializeField]
    [Tooltip("First Camera")]
    private GameObject firstCam = default;

    [SerializeField]
    [Tooltip("Second Camera")]
    private GameObject secondCam = default;

    [SerializeField]
    [Tooltip("First level GameObject")]
    private GameObject firstLevel = default;

    [SerializeField]
    [Tooltip("Second level GameObject")]
    private GameObject secondLevel = default;
    #endregion

    #region UI
    [Space, Header("UI")]
    [SerializeField]
    [Tooltip("Menu Buttons")]
    private Button[] menuButtons = default;

    [SerializeField]
    [Tooltip("Menu panel GameObject")]
    private GameObject menuPanel = default;

    [SerializeField]
    [Tooltip("Game panel GameObject")]
    private GameObject gamePanel = default;

    [SerializeField]
    [Tooltip("Fade Image Animation Component")]
    private Animator fadeBG = default;

    [SerializeField]
    [Tooltip("Intro Timeline")]
    private PlayableDirector introTimeline;

    [SerializeField]
    [Tooltip("First Panel")]
    private GameObject firstPanel = default;

    [SerializeField]
    [Tooltip("Second Panel")]
    private GameObject secondPanel = default;
    #endregion

    #region Audio
    [SerializeField]
    [Tooltip("Heartbeat Aud SFX")]
    private AudioSource stableHeartbeatAud = default;

    [SerializeField]
    [Tooltip("Heartbeat Flat SFX")]
    private AudioClip flatHeartbeatSFX = default;
    #endregion

    #endregion

    #region Private Variables
    private float _currTapSpeed = default;
    [SerializeField] private bool _isGameRunning = default;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        DoggoController.OnPlayerDead += OnPlayerDeadEventReceived;
        DoggoController.OnPlayerFinish += OnPlayerFinishEventReceived;
    }

    void OnDisable()
    {
        DoggoController.OnPlayerDead -= OnPlayerDeadEventReceived;
        DoggoController.OnPlayerFinish -= OnPlayerFinishEventReceived;
    }

    void OnDestroy()
    {
        DoggoController.OnPlayerDead -= OnPlayerDeadEventReceived;
        DoggoController.OnPlayerFinish -= OnPlayerFinishEventReceived;
    }
    #endregion

    void Start()
    {
        fadeBG.Play("Fade_In");
        EnableCursor();
    }

    void Update()
    {
        if (_currGameState == GameState.Intro)
            TapCounter();

        EnergyBar();
    }
    #endregion

    #region My Functions
    /// <summary>
    /// Tied to Start_Button;
    /// Starts the game when button is pressed;
    /// </summary>
    public void OnClick_StartGame()
    {
        DisableCursor();
        menuPanel.SetActive(false);
        introTimeline.Play();
    }

    /// <summary>
    /// Tied to Exit Button;
    /// Exits the game with Delay;
    /// </summary>
    public void OnClick_ExitGame() => StartCoroutine(QuitDelay());

    public void OnStartRunning()
    {
        _currGameState = GameState.Intro;
        gamePanel.SetActive(true);
    }

    /// <summary>
    /// Disables all the Button interaction in the scene;
    /// </summary>
    public void DisableButtons()
    {
        for (int i = 0; i < menuButtons.Length; i++)
            menuButtons[i].interactable = false;
    }

    /// <summary>
    /// Increments float value by 1 when right or left is pressed;
    /// </summary>
    void TapCounter()
    {
        if (Input.GetKeyDown(KeyCode.A) /*|| Input.GetKeyDown(KeyCode.LeftArrow)*/)
            _currTapSpeed++;

        if (Input.GetKeyDown(KeyCode.D) /*|| Input.GetKeyDown(KeyCode.RightArrow)*/)
            _currTapSpeed++;

        playerRootAnim.SetFloat("Speed", _currTapSpeed);
    }

    /// <summary>
    /// Sets the clamp of the float value;
    /// Updates the UI of the Slider;
    /// </summary>
    void EnergyBar()
    {
        _currTapSpeed -= Time.deltaTime * decrementSpeed;
        _currTapSpeed = Mathf.Clamp(_currTapSpeed, 0, 10);

        energyBar.value = _currTapSpeed;

        if (_currTapSpeed >= 10 && !_isGameRunning)
        {
            _currGameState = GameState.Game;
            _isGameRunning = true;
            StartCoroutine(SwitchLevelDelay());
        }
    }

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
    IEnumerator QuitDelay()
    {
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(0.8f);
        stableHeartbeatAud.Stop();
        stableHeartbeatAud.PlayOneShot(flatHeartbeatSFX);
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(5f);
        Application.LoadLevel(Application.loadedLevel);
    }

    IEnumerator SwitchLevelDelay()
    {
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        firstLevel.SetActive(false);
        firstCam.SetActive(false);
        firstPanel.SetActive(false);
        secondLevel.SetActive(true);
        secondCam.SetActive(true);
        secondPanel.SetActive(true);
        runningDogObj.SetActive(true);
        fadeBG.Play("Fade_In");
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