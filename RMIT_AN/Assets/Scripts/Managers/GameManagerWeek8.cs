using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameManagerWeek8 : MonoBehaviour
{
    #region Serialized Variables

    #region Enums
    [Space, Header("Enums")]
    [SerializeField] private GameState _currGameState = GameState.Intro;
    private enum GameState { Intro, Game, Paused, Outro };
    #endregion

    #region UI
    [Space, Header("UI")]
    [SerializeField]
    [Tooltip("Pause Buttons")]
    private Button[] pauseButtons = default;

    [SerializeField]
    [Tooltip("Pause Panel")]
    private GameObject pausePanel;

    [SerializeField]
    [Tooltip("HUD Panel")]
    private GameObject hudPanel;

    [SerializeField]
    [Tooltip("Fade Image Animation Component")]
    private Animator fadeBG = default;
    #endregion

    #region Game
    [Space, Header("Game")]
    [SerializeField]
    [Tooltip("Beds")]
    private GameObject[] beds = default;

    [SerializeField]
    [Tooltip("Player")]
    private GameObject playerRoot = default;

    [SerializeField]
    [Tooltip("Player Cinematic Camera")]
    private GameObject playerCinematicCam = default;

    [SerializeField]
    [Tooltip("Game End after how many Bed Checks?")]
    private int gameEndBedCheck = 4;

    [SerializeField]
    [Tooltip("Audio Clips for Bed Party")]
    private AudioClip[] bedPartySFX = default;

    [SerializeField]
    [Tooltip("Timeline Intro")]
    private PlayableDirector timelineIntro = default;
    #endregion

    #endregion

    #region Private Variables
    private const string _interactLayer = "Door_Layer";
    private int _currBedsChecked = default;
    private List<AudioSource> bedPartyAud = new List<AudioSource>();
    private List<Light> bedLight = new List<Light>();
    private int _currBedAud = default;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        BedInteraction.OnBedInteract += OnBedInteractEventReceived;

        FPSControllerBasic.OnPlayerDead += OnPlayerDeadEventReceived;
    }

    void OnDisable()
    {
        BedInteraction.OnBedInteract -= OnBedInteractEventReceived;

        FPSControllerBasic.OnPlayerDead -= OnPlayerDeadEventReceived;
    }

    void OnDestroy()
    {
        BedInteraction.OnBedInteract -= OnBedInteractEventReceived;

        FPSControllerBasic.OnPlayerDead -= OnPlayerDeadEventReceived;
    }
    #endregion

    void Start()
    {
        StartCoroutine(StartDelay());
        DisableCursor();
    }

    void Update()
    {
        if (_currGameState == GameState.Game)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePause(true);
        }
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

    #region UI
    void TogglePause(bool isPaused)
    {
        if (isPaused)
        {
            _currGameState = GameState.Paused;
            EnableCursor();
            Time.timeScale = 0;
            hudPanel.SetActive(false);
            pausePanel.SetActive(true);
        }
        else
        {
            _currGameState = GameState.Game;
            DisableCursor();
            Time.timeScale = 1;
            hudPanel.SetActive(true);
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

    /// <summary>
    /// Intialises the Bed Audio and Light Components on Start;
    /// Sets the First bed to show the bed light;
    /// </summary>
    void IntialiseBed()
    {
        for (int i = 0; i < beds.Length; i++)
        {
            bedLight.Add(beds[i].GetComponentInChildren<Light>());
            bedPartyAud.Add(beds[i].GetComponent<AudioSource>());
        }

        beds[0].layer = LayerMask.NameToLayer(_interactLayer);
        bedLight[0].enabled = true;

        bedPartyAud[0].clip = bedPartySFX[2];
        bedPartyAud[0].Play();
        _currBedAud = 0;

        //ChooseBed();
    }

    void ChooseBed()
    {
        bedPartyAud[_currBedAud].Stop();
        int bedIndex = Random.Range(0, beds.Length);
        int sfxIndex = Random.Range(0, bedPartySFX.Length);

        beds[bedIndex].layer = LayerMask.NameToLayer(_interactLayer);
        bedLight[bedIndex].enabled = true;

        bedPartyAud[bedIndex].clip = bedPartySFX[sfxIndex];
        bedPartyAud[bedIndex].Play();
        _currBedAud = bedIndex;

        Debug.Log(bedIndex);
    }

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
        timelineIntro.Play();
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
    /// <summary>
    /// Subbed to event from BedInteraction Script
    /// Chooses the next bed to light up;
    /// </summary>
    void OnBedInteractEventReceived()
    {
        _currBedsChecked++;

        if (_currBedsChecked < gameEndBedCheck)
            ChooseBed();

        if (_currBedsChecked >= gameEndBedCheck)
        {
            OnClick_Menu();
            Debug.Log("Game Ended");
        }
    }

    /// <summary>
    /// Subbed to event from FPSControllerBasic Script
    /// Restarts the Game with delay;
    /// </summary>
    void OnPlayerDeadEventReceived() => StartCoroutine(RestartGameDelay());

    /// <summary>
    /// Subbed to event on Timeline;
    /// When intro ends, the Player gets control to move around;
    /// </summary>
    public void OnIntroEnd()
    {
        playerRoot.SetActive(true);
        timelineIntro.gameObject.SetActive(false);
        playerCinematicCam.SetActive(false);
        IntialiseBed();
        _currGameState = GameState.Game;
    }
    #endregion
}