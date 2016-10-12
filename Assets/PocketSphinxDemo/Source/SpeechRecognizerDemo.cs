using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpeechRecognizerDemo : MonoBehaviour, IPocketSphinxEvents
{

    /* Named searches allow to quickly reconfigure the decoder */
    private const String KWS_SEARCH = "wakeup";
    private const String FORECAST_SEARCH = "forecast";
    private const String DIGITS_SEARCH = "digits";
    private const String PHONE_SEARCH = "phones";
    private const String MENU_SEARCH = "menu";

    /* Keyword we are looking for to activate menu */
    private const String KEYPHRASE = "oh mighty computer";

    #region Public serialized fields
    [SerializeField]
    private GameObject _pocketSphinxPrefab;
    [SerializeField]
    private Text _infoText;
    [SerializeField]
    private Text _SpeechResult;
    [SerializeField]
    private string[] progressTexts;
    #endregion

    #region Private fields
    private UnityPocketSphinx.PocketSphinx _pocketSphinx;

    private Dictionary<string, string> infoTextDict;
    #endregion

    #region Private methods
    private void SubscribeToPocketSphinxEvents()
    {
        EM_UnityPocketsphinx em = _pocketSphinx.EventManager;

        em.OnBeginningOfSpeech += OnBeginningOfSpeech;
        em.OnEndOfSpeech += OnEndOfSpeech;
        em.OnError += OnError;
        em.OnInitializeFailed += OnInitializeFailed;
        em.OnInitializeSuccess += OnInitializeSuccess;
        em.OnPartialResult += OnPartialResult;
        em.OnPocketSphinxError += OnPocketSphinxError;
        em.OnResult += OnResult;
        em.OnTimeout += OnTimeout;
    }

    private void UnsubscribeFromPocketSphinxEvents()
    {
        EM_UnityPocketsphinx em = _pocketSphinx.EventManager;

        em.OnBeginningOfSpeech -= OnBeginningOfSpeech;
        em.OnEndOfSpeech -= OnEndOfSpeech;
        em.OnError -= OnError;
        em.OnInitializeFailed -= OnInitializeFailed;
        em.OnInitializeSuccess -= OnInitializeSuccess;
        em.OnPartialResult -= OnPartialResult;
        em.OnPocketSphinxError -= OnPocketSphinxError;
        em.OnResult -= OnResult;
        em.OnTimeout -= OnTimeout;
    }

    private void switchSearch(string searchKey)
    {
        _pocketSphinx.StopRecognizer();

        if (searchKey.Equals(KWS_SEARCH))
        {
            _pocketSphinx.StartListening(searchKey);
        }
        else
        {
            _pocketSphinx.StartListening(searchKey, 10000);
        }

        string text;
        infoTextDict.TryGetValue(searchKey, out text);

        _infoText.text = text;
        _SpeechResult.text = "Say something!";
    }
    #endregion

    #region MonoBehaviour methods
    void Awake()
    {
        UnityEngine.Assertions.Assert.IsNotNull(_pocketSphinxPrefab, "No PocketSphinx prefab assigned.");
        var obj = Instantiate(_pocketSphinxPrefab, this.transform) as GameObject;
        _pocketSphinx = obj.GetComponent<UnityPocketSphinx.PocketSphinx>();

        if (_pocketSphinx == null)
        {
            Debug.LogError("[SpeechRecognizerDemo] No PocketSphinx component found. Did you assign the right prefab???");
        }

        SubscribeToPocketSphinxEvents();

        _infoText.text = "Please wait for Speech Recognition engine to load.";
        _SpeechResult.text = "Loading human dictionary...";
    }

    void Start()
    {
        _pocketSphinx.SetAcousticModelPath("en-us-ptm");
        //Debug.Log("[SpeechRecognizerDemo] " + Application.streamingAssetsPath + "cmudict-en-us.dict");
        _pocketSphinx.SetDictionaryPath("cmudict-en-us.dict");
        _pocketSphinx.SetKeywordThreshold(1e-45f);
        _pocketSphinx.AddBoolean("-allphone_ci", true);

        // These one are optional
        _pocketSphinx.AddGrammarSearchPath(MENU_SEARCH, "menu.gram");
        _pocketSphinx.AddGrammarSearchPath(DIGITS_SEARCH, "digits.gram");
        _pocketSphinx.AddNGramSearchPath(FORECAST_SEARCH, "weather.dmp");
        _pocketSphinx.AddAllPhoneSearchPath(PHONE_SEARCH, "en-phone.dmp");

        _pocketSphinx.SetupRecognizer();

        infoTextDict = new Dictionary<string, string>();
        infoTextDict.Add(KWS_SEARCH, progressTexts[0]);
        infoTextDict.Add(MENU_SEARCH, progressTexts[1]);
        infoTextDict.Add(DIGITS_SEARCH, progressTexts[2]);
        infoTextDict.Add(FORECAST_SEARCH, progressTexts[3]);
        infoTextDict.Add(PHONE_SEARCH, progressTexts[4]);
    }
    
    // Update is called once per frame
    void Update ()
    {
        
    }

    void OnDestroy()
    {
        if (_pocketSphinx != null)
        {
            UnsubscribeFromPocketSphinxEvents();
            _pocketSphinx.DestroyRecognizer();
        }
    }
    #endregion

    #region PocketSphinx event methods
    public void OnPartialResult(string hypothesis)
    {
        _SpeechResult.text = hypothesis;
        if (hypothesis.Equals(KEYPHRASE))
            switchSearch(MENU_SEARCH);
        else if (hypothesis.Equals(DIGITS_SEARCH))
            switchSearch(DIGITS_SEARCH);
        else if (hypothesis.Equals(PHONE_SEARCH))
            switchSearch(PHONE_SEARCH);
        else if (hypothesis.Equals(FORECAST_SEARCH))
            switchSearch(FORECAST_SEARCH);
        else
        {
            _SpeechResult.text = hypothesis;
        }
    }

    public void OnResult(string hypothesis)
    {
        _SpeechResult.text = hypothesis;
    }

    public void OnBeginningOfSpeech()
    {
        
    }

    public void OnEndOfSpeech()
    {
        switchSearch(KWS_SEARCH);
    }

    public void OnError(string error)
    {
        Debug.LogError("[SpeechRecognizerDemo] An error ocurred at OnError()");
        Debug.LogError("[SpeechRecognizerDemo] error = " + error);
    }

    public void OnTimeout()
    {
        Debug.Log("[SpeechRecognizerDemo] Speech Recognition timed out");
        switchSearch(KWS_SEARCH);
    }

    public void OnInitializeSuccess()
    {
        _pocketSphinx.AddKeyphraseSearch(KWS_SEARCH, KEYPHRASE);
        switchSearch(KWS_SEARCH);
    }

    public void OnInitializeFailed(string error)
    {
        Debug.LogError("[SpeechRecognizerDemo] An error ocurred on Initialization PocketSphinx.");
        Debug.LogError("[SpeechRecognizerDemo] error = " + error);
    }

    public void OnPocketSphinxError(string error)
    {
        Debug.LogError("[SpeechRecognizerDemo] An error ocurred on OnPocketSphinxError().");
        Debug.LogError("[SpeechRecognizerDemo] error = " + error);
    }
    #endregion
}
