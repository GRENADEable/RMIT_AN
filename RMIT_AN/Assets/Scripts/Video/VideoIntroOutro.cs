using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoIntroOutro : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("Fade Image Animation Component")]
    private Animator fadeBG = default;

    [SerializeField]
    [Tooltip("Scene Numbner")]
    private int sceneNo = default;
    #endregion

    #region Private Variables
    private VideoPlayer _vidPlayer = default;
    private
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        _vidPlayer.loopPointReached += OnLoopPointReachedEventReceived;
    }

    void OnDisable()
    {
        _vidPlayer.loopPointReached -= OnLoopPointReachedEventReceived;
    }

    void OnDestroy()
    {
        _vidPlayer.loopPointReached -= OnLoopPointReachedEventReceived;
    }
    #endregion

    void Awake()
    {
        _vidPlayer = GetComponent<VideoPlayer>();
        fadeBG.Play("Fade_In");
    }
    #endregion

    #region My Functions

    #endregion

    #region Coroutines
    IEnumerator EndDelay()
    {
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(sceneNo);
    }
    #endregion

    #region Events
    void OnLoopPointReachedEventReceived(VideoPlayer vid) => StartCoroutine(EndDelay());
    #endregion
}