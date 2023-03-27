using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string format;
    
    private void Awake()
    {
        StartCoroutine(SubscribeOnScoreUpdate());
    }

    private void OnDestroy()
    {
        ScoresHandler.Instance.OnScoreUpdated -= ScoreUpdated;
    }

    private IEnumerator SubscribeOnScoreUpdate()
    {
        yield return null;
        ScoresHandler.Instance.OnScoreUpdated += ScoreUpdated;
        ScoreUpdated(ScoresHandler.Instance.Score);
    }

    private void ScoreUpdated(float score)
    {
        text.text = String.Format(format, score);
    }
}
