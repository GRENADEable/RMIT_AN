using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Serialized Variables

    [Space, Header("Enums")]
    [Tooltip("Current Game State")]
    [SerializeField] private GameState _currGameState = GameState.Menu;
    private enum GameState { Menu, Game, End, Exit };

    [Space, Header("Player")]
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
    #endregion

    #region Private Variables
    [SerializeField] private float _currTapSpeed = default;
    #endregion

    #region Unity Callbacks
    void Start() => fadeBG.Play("Fade_In");

    void Update()
    {
        if (_currGameState == GameState.Game)
            TapCounter();

        _currTapSpeed -= Time.deltaTime * decrementSpeed;
        _currTapSpeed = Mathf.Clamp(_currTapSpeed, 0, 10);

        energyBar.value = _currTapSpeed;
    }
    #endregion

    #region My Functions
    /// <summary>
    /// Tied to Start_Button;
    /// Starts the game when button is pressed;
    /// </summary>
    public void OnClick_StartGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        _currGameState = GameState.Game;
    }

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
    #endregion

    #region Coroutines
    IEnumerator QuitDelay()
    {
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    #endregion
}