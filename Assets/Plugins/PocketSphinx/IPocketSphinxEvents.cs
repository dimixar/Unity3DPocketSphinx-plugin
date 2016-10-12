using UnityEngine;
using System.Collections;

public interface IPocketSphinxEvents 
{
    void OnPartialResult(string hypothesis);

    void OnResult(string hypothesis);

    void OnBeginningOfSpeech();

    void OnEndOfSpeech();

    void OnError(string error);

    void OnTimeout();

    void OnInitializeSuccess();

    void OnInitializeFailed(string error);

    void OnPocketSphinxError(string error);
}
