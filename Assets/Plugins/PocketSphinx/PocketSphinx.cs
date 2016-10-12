using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityPocketSphinx
{
    public class PocketSphinx : MonoBehaviour
    {
        #region Public serialized fields
        [SerializeField]
        private GameObject _eventManagerPrefab;
        #endregion

        #region Private fields
        private EM_UnityPocketsphinx _eventManager;
        private IPocketSphinx _pocketSphinxInst;
        #endregion

        #region Public methods and properties
        public EM_UnityPocketsphinx EventManager
        {
            get { return _eventManager; }
        }

        public void AddAllPhoneSearchPath(string searchKey, string phonePath)
        {
            if (_pocketSphinxInst == null)
                return;
            
            _pocketSphinxInst.AddAllPhoneSearchPath(searchKey, phonePath);
        }

        public void AddBoolean(string arg, bool enabled)
        {
            if (_pocketSphinxInst == null)
                return;
            
            _pocketSphinxInst.AddBoolean(arg, enabled);
        }

        public void AddGrammarSearchPath(string searchKey, string grammarPath)
        {
            if (_pocketSphinxInst == null)
                return;

            _pocketSphinxInst.AddGrammarSearchPath(searchKey, grammarPath);
        }

        public void AddKeyphraseSearch(string searchKey, string keyphrase)
        {
            if (_pocketSphinxInst == null)
                return;

            _pocketSphinxInst.AddKeyphraseSearch(searchKey, keyphrase);
        }

        public void AddNGramSearchPath(string searchKey, string nGramPath)
        {
            if (_pocketSphinxInst == null)
                return;

            _pocketSphinxInst.AddNGramSearchPath(searchKey, nGramPath);
        }

        public void CancelRecognizer()
        {
            if (_pocketSphinxInst == null)
                return;

            _pocketSphinxInst.CancelRecognizer();
        }

        public void DestroyRecognizer()
        {
            if (_pocketSphinxInst == null)
                return;

            _pocketSphinxInst.DestroyRecognizer();
        }

        public void SetAcousticModelPath(string dirPath)
        {
            if (_pocketSphinxInst == null)
                return;

            _pocketSphinxInst.SetAcousticModelPath(dirPath);
        }

        public void SetDictionaryPath(string dictPath)
        {
            if (_pocketSphinxInst == null)
                return;

            _pocketSphinxInst.SetDictionaryPath(dictPath);
        }

        public void SetKeywordThreshold(float thres)
        {
            if (_pocketSphinxInst == null)
                return;

            _pocketSphinxInst.SetKeywordThreshold(thres);
        }

        public void SetupRecognizer()
        {
            if (_pocketSphinxInst == null)
                return;

            _pocketSphinxInst.SetupRecognizer();
        }

        public void StartListening(string searchKey)
        {
            if (_pocketSphinxInst == null)
                return;

            _pocketSphinxInst.StartListening(searchKey);
        }

        public void StartListening(string searchKey, int timeout)
        {
            if (_pocketSphinxInst == null)
                return;

            _pocketSphinxInst.StartListening(searchKey, timeout);
        }

        public void StopRecognizer()
        {
            if (_pocketSphinxInst == null)
                return;

            _pocketSphinxInst.StopRecognizer();
        }
        #endregion

        #region MonoBehaviour methods
        void Awake()
        {
            UnityEngine.Assertions.Assert.IsNotNull(_eventManagerPrefab, "UnityPocketSphinx Event Manager is not assigned.");
            var obj = Instantiate(_eventManagerPrefab, this.transform) as GameObject;
            _eventManager = obj.GetComponent<EM_UnityPocketsphinx>();

            if (_eventManager == null)
            {
                Debug.LogError("[PocketSphinx] No EM_UnityPocketsphinx component found. Did you assign the right eventManager prefab???");
            }

            #if UNITY_ANDROID && !UNITY_EDITOR
            _pocketSphinxInst = new PocketSphinx_Android();
            #elif UNITY_IOS
            Debug.LogError("[PocketSphinx] IOS version not yet implemented.");
            _pocketSphinxInst = null;
            #endif
        }
        #endregion
    }
}
