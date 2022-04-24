using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerWeek7 : MonoBehaviour
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
    #endregion

    #region Game
    [Space, Header("Game Timer")]
    [SerializeField]
    [Tooltip("After how many seconds does the ship horn play?")]
    private float hornTime = default;

    [SerializeField]
    [Tooltip("After how many seconds does the game end?")]
    private float endTime = default;
    #endregion

    #region Audio
    [Space, Header("Audios")]
    [SerializeField]
    [Tooltip("Horn Aud Source")]
    private AudioSource hornAud = default;

    [SerializeField]
    [Tooltip("Horn Aud Source")]
    private float hornPitchAud = default;

    [SerializeField]
    [Tooltip("Horn SFXs")]
    private AudioClip[] sfxClips;
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
        StartCoroutine(HornDelay());
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

    #region Game
    IEnumerator HornDelay()
    {
        yield return new WaitForSeconds(hornTime);
        hornAud.PlayOneShot(sfxClips[0]);
        yield return new WaitForSeconds(hornTime);
        hornAud.PlayOneShot(sfxClips[1]);
        yield return new WaitForSeconds(hornTime);
        hornAud.pitch = hornPitchAud;
        hornAud.PlayOneShot(sfxClips[2]);
        yield return new WaitForSeconds(endTime);
        hornAud.Stop();
        fadeBG.Play("Fade_Out");
        hornAud.pitch = 1f;
        hornAud.PlayOneShot(sfxClips[3]);
        yield return new WaitForSeconds(1.5f);
        Application.LoadLevel(3);
    }
    #endregion

    #endregion
}