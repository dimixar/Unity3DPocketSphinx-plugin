using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// This class will receive and send caught events from pocketsphinx plugin.
/// UnitySphinxObject has to subscribe to it's public events
/// </summary>
public class EM_UnityPocketsphinx : MonoBehaviour 
{
    private const string gameObjName = "EM_UnityPocketSphinx";

    #region Public events
    /// <summary>
    /// In partial result we get quick updates about current hypothesis. In
    /// keyword spotting mode we can react here, in other modes we need to wait
    /// for final result in onResult.
    /// </summary>
    public event Action<string> OnPartialResult;

    /// <summary>
    /// This callback is called when we stop the recognizer.
    /// </summary>
    public event Action<string> OnResult;

    /// <summary>
    /// This callback is called when user starts to speak.
    /// </summary>
    public event Action OnBeginningOfSpeech;

    /// <summary>
    /// This callback is called when user finishes to speak.
    /// NOTE: Here you should call StopRecognizer() to get results.
    /// </summary>
    public event Action OnEndOfSpeech;
    public event Action<string> OnError;
    public event Action OnTimeout;

    /// <summary>
    /// This callback is called when pocketsphinx was successfully initialized.
    /// </summary>
    public event Action OnInitializeSuccess;

    /// <summary>
    /// This callback is called when pocketsphinx failed to initialize.
    /// </summary>
    public event Action<string> OnInitializeFailed;

    /// <summary>
    /// This callback is called when pocketsphinx has somekind of error.
    /// </summary>
    public event Action<string> OnPocketSphinxError;
    #endregion

    #region Public Methods called by plugin
    public void PSOnPartialResult(string hyp)
    {
        Debug.Log("[EM_UnityPocketsphinx] OnPartialResult() called.");
        if (OnPartialResult != null)
        {
            OnPartialResult(hyp);
        }
    }

    public void PSOnResult(string hyp)
    {
        Debug.Log("[EM_UnityPocketsphinx] OnResult() called.");
        if (OnResult != null)
        {
            OnResult(hyp);
        }
    }

    public void PSOnBeginningOfSpeech(string mes)
    {
        Debug.Log("[EM_UnityPocketsphinx] OnBeginningOfSpeech() called.");
        Debug.Log("[EM_UnityPocketsphinx] " + mes);
        if (OnBeginningOfSpeech != null)
        {
            OnBeginningOfSpeech();
        }
    }

    public void PSOnEndOfSpeech(string mes)
    {
        Debug.Log("[EM_UnityPocketsphinx] OnEndOfSpeech() called.");
        Debug.Log("[EM_UnityPocketsphinx] " + mes);
        if (OnEndOfSpeech != null)
        {
            OnEndOfSpeech();
        }
    }

    public void PSOnError(string err)
    {
        Debug.Log("[EM_UnityPocketSphinx] OnError() called.");
        if (OnError != null)
        {
            OnError(err);
        }
    }

    public void PSOnTimeout(string mes)
    {
        Debug.Log("[EM_UnityPocketsphinx] OnTimeout() called.");
        Debug.Log("[EM_UnityPocketsphinx] " + mes);
        if (OnTimeout != null)
        {
            OnTimeout();
        }
    }

    public void PSOnInitializeSuccess(string mes)
    {
        Debug.Log("[EM_UnityPocketsphinx] OnInitializeSuccess() called.");
        Debug.Log("[EM_UnityPocketsphinx] " + mes);
        if (OnInitializeSuccess != null)
        {
            OnInitializeSuccess();
        }
    }

    public void PSOnInitializeFailed(string err)
    {
        Debug.Log("[EM_UnityPocketSphinx] OnInitializeFailed() called.");
        if (OnInitializeFailed != null)
        {
            OnInitializeFailed(err);
        }
    }

    public void PSOnPocketSphinxError(string err)
    {
        Debug.Log("[EM_UnityPocketSphinx] OnPocketSphinxError() called.");
        if (OnPocketSphinxError != null)
        {
            OnPocketSphinxError(err);
        }
    }
    #endregion

    #region MonoBehaviour methods
    void Start()
    {
        gameObject.name = gameObjName;
    }
    #endregion
}
