using System.Collections;
using UnityEngine;

public class Safepoint : MonoBehaviour
{
    [SerializeField] private int scorePerSeconds;

    private const float ScoreUpdateTime = 1.0f;

    private Coroutine _scoreUpdateCoroutine;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            StopScoringCoroutine();
            _scoreUpdateCoroutine = StartCoroutine(ScoringCoroutine());
        }
    }
    
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            StopScoringCoroutine();
        }
    }

    private void StopScoringCoroutine()
    {
        if (_scoreUpdateCoroutine != null)
        {
            StopCoroutine(_scoreUpdateCoroutine);
            _scoreUpdateCoroutine = null;
        }
    }

    private void UpdateScore(int score)
    {
        ScoresHandler.Instance.AddScore(score);
    }

    private IEnumerator ScoringCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(ScoreUpdateTime);
            UpdateScore(scorePerSeconds);
        }
    }
}