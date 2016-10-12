using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System;

namespace UnityPocketSphinx
{
    public class PocketSphinx_Android : IPocketSphinx
    {
        #region private fields
        private AndroidJavaObject _pocketSphinxActivityObj;
        #endregion

        #region private methods
        private void AssertPocketSphinxActivityObj()
        {
            UnityEngine.Assertions.Assert.IsNull<AndroidJavaObject>(
                _pocketSphinxActivityObj,
                "[PocketSphinx_Android] Could not get instance reference.");
        }

        private void CallPocketSphinxMethod(string methodName, params object[] args)
        {
            //AssertPocketSphinxActivityObj();
            using (AndroidJavaClass pocketSphinxClass = new AndroidJavaClass("edu.cmu.pocketsphinx.unityWrap.PocketSphinxActivity"))
            {
                Debug.Log("[PocketSphinx_Android] Got pocketSphinxClass.");
                using (_pocketSphinxActivityObj = pocketSphinxClass.CallStatic<AndroidJavaObject>("getInstance"))
                {
                    Debug.Log("[PocketSphinx_Android] Got _pocketSphinxActivityObj.");
                    _pocketSphinxActivityObj.Call(methodName, args);
                    Debug.Log("[PocketSphinx_Android] Called " + methodName + "().");
                }
            }
        }
        #endregion

        #region public methods and properties
        public PocketSphinx_Android()
        {
            using (AndroidJavaClass pocketSphinxClass = new AndroidJavaClass("edu.cmu.pocketsphinx.unityWrap.PocketSphinxActivity"))
            {
                Debug.Log("[PocketSphinx_Android] Creating PocketSphinxActivity java object.");
                _pocketSphinxActivityObj = pocketSphinxClass.CallStatic<AndroidJavaObject>("getInstance");
                // Get activity instance (standard way, solid)
                var pl_class = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                var currentActivity = pl_class.GetStatic<AndroidJavaObject>("currentActivity");
                _pocketSphinxActivityObj.Call("SetActivityContext", currentActivity);

                Debug.Log("[PocketSphinx_Android] Created PocketSphinxActivity java object.");
            };

            //AssertPocketSphinxActivityObj();
        }
        #endregion

        #region public PocketSphinx interface methods
        public void AddAllPhoneSearchPath(string searchKey, string phonePath)
        {
            Debug.Log("[PocketSphinx_Android] Calling AddAllPhoneSearchPath().");
            CallPocketSphinxMethod("AddAllPhoneSearchPath", searchKey, phonePath);
        }

        public void AddBoolean(string arg, bool enabled)
        {
            Debug.Log("[PocketSphinx_Android] Calling AddBoolean()");
            CallPocketSphinxMethod("AddBoolean", arg, enabled);
        }

        public void AddGrammarSearchPath(string searchKey, string grammarPath)
        {
            Debug.Log("[PocketSphinx_Android] Calling AddGrammarSearchPath()");
            CallPocketSphinxMethod("AddGrammarSearchPath", searchKey, grammarPath);
        }

        public void AddKeyphraseSearch(string searchKey, string keyphrase)
        {
            Debug.Log("[PocketSphinx_Android] Calling AddKeyphraseSearch()");
            CallPocketSphinxMethod("AddKeyphraseSearch", searchKey, keyphrase);
        }

        public void AddNGramSearchPath(string searchKey, string nGramPath)
        {
            Debug.Log("[PocketSphinx_Android] Calling AddNGramSearchPath()");
            CallPocketSphinxMethod("AddNGramSearchPath", searchKey, nGramPath);
        }

        public void CancelRecognizer()
        {
            Debug.Log("[PocketSphinx_Android] Calling CancelRecognizer()");
            CallPocketSphinxMethod("CancelRecognizer");
        }

        public void DestroyRecognizer()
        {
            Debug.Log("[PocketSphinx_Android] Calling DestroyRecognizer()");
            CallPocketSphinxMethod("DestroyRecognizer");
        }

        public void SetAcousticModelPath(string dirPath)
        {
            Debug.Log("[PocketSphinx_Android] Calling SetAcousticModelPath()");
            CallPocketSphinxMethod("SetAcousticModelPath", dirPath);
        }

        public void SetDictionaryPath(string dictPath)
        {
            Debug.Log("[PocketSphinx_Android] Calling SetDictionaryPath()");
            CallPocketSphinxMethod("SetDictionaryPath", dictPath);
        }

        public void SetKeywordThreshold(float thres)
        {
            Debug.Log("[PocketSphinx_Android] Calling SetKeywordThreshold()");
            CallPocketSphinxMethod("SetKeywordThreshold", thres);
        }

        public void SetupRecognizer()
        {
            Debug.Log("[PocketSphinx_Android] Calling SetupRecognizer()");
            CallPocketSphinxMethod("RunRecognizerSetup");
        }

        public void StartListening(string searchKey)
        {
            Debug.Log("[PocketSphinx_Android] Calling StartListening(string)");
            CallPocketSphinxMethod("StartListening", searchKey);
        }

        public void StartListening(string searchKey, int timeout)
        {
            Debug.Log("[PocketSphinx_Android] Calling StartListening(string, int)");
            CallPocketSphinxMethod("StartListening", searchKey, timeout);
        }

        public void StopRecognizer()
        {
            Debug.Log("[PocketSphinx_Android] Calling StopRecognizer()");
            CallPocketSphinxMethod("StopRecognizer");
        }

        #endregion
    }
}

