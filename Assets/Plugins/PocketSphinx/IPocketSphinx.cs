using UnityEngine;
using System.Collections;

namespace UnityPocketSphinx
{
    public interface IPocketSphinx
    {
        #region Public methods
        void SetAcousticModelPath(string dirPath);
        void SetDictionaryPath(string dictPath);

        /// <summary>
        /// Threshold to tune for keyphrase to balance between false alarms and misses.
        /// </summary>
        /// <param name="thres">in milliseconds.</param>
        void SetKeywordThreshold(float thres);

        /// <summary>
        /// Adds an boolean argument option for PocketSphinx.
        /// </summary>
        /// <param name="arg">additional boolean argument.</param>
        /// <param name="enabled">boolean argument state.</param>
        void AddBoolean(string arg, bool enabled);

        /// <summary>
        /// Add grammar-based search for selection with searchKey.
        /// </summary>
        /// <param name="searchKey">Listens to grammar associated with this key.</param>
        /// <param name="grammarPath">Path grammar file (*.gram).</param>
        void AddGrammarSearchPath(string searchKey, string grammarPath);

        /// <summary>
        /// Add language model search for selection with searchKey.
        /// </summary>
        /// <param name="searchKey">Listens to language model associated with this key.</param>
        /// <param name="nGramPath">Path nGram file (*.dmp).</param>
        void AddNGramSearchPath(string searchKey, string nGramPath);

        /// <summary>
        /// Add Phonetic search for selection with searchKey.
        /// </summary>
        /// <param name="searchKey">Listens to phonetic data associated with this key.</param>
        /// <param name="phonePath">Path to phonetic file (*.dmp).</param>
        void AddAllPhoneSearchPath(string searchKey, string phonePath);

        /// <summary>
        /// Adds a specific keyphrase for selection with searchKey.
        /// NOTE: Call this method only when the recognizer is already initialized, after calling the SetupRecognizer() method.
        /// </summary>
        /// <param name="searchKey">Listens to keyphrase associated with this key.</param>
        /// <param name="keyphrase">What keyphrase to listen.</param>
        void AddKeyphraseSearch(string searchKey, string keyphrase);

        /// <summary>
        /// Listens only what is setup in searchKey
        /// </summary>
        /// <param name="searchKey">Based on it will choose what to listen to.</param>
        void StartListening(string searchKey);

        /// <summary>
        /// Same as StartListening without timeout
        /// </summary>
        /// <param name="searchKey">Based on it will choose what to listen to.</param>
        /// <param name="timeout">Will listen for some milliseconds.</param>
        void StartListening(string searchKey, int timeout);

        /// <summary>
        /// Stops speech recognition from further listening.
        /// NOTE: This should be called every time "OnEndOfSpeech" event triggers, and
        /// will trigger "OnResult" event that will give the final hypothesis.
        /// </summary>
        void StopRecognizer();

        /// <summary>
        /// Cancels out speech recognition.
        /// NOTE: Can be called anytime, but will not give out the final found hypothesis.
        /// </summary>
        void CancelRecognizer();

        /// <summary>
        /// Destroys the current speech recognition configuration.
        /// NOTE: Should be called from C# to properly deallocate everything Pocketsphinx related.
        /// </summary>
        void DestroyRecognizer();

        /// <summary>
        /// Will setup speech recognizer and trigger one of 2 possible events
        /// </summary>
        void SetupRecognizer();
        #endregion
    }
}

