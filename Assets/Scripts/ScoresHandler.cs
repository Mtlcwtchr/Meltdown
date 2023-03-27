using System;
using UnityEngine;

public class ScoresHandler : MonoBehaviour
{
     public event Action<float> OnScoreUpdated;
     
     private static ScoresHandler _instance;
     public static ScoresHandler Instance => _instance;
     
     private IPlayerDatabase _playerDatabase;

     private int? _score;
     public int Score
     {
          get => _score ??= _playerDatabase.GetScores();
          set
          {
               _score = value;
               _playerDatabase.SetScores(_score ?? 0);
               OnScoreUpdated?.Invoke(_score ?? 0);
          }
     }

     private void Awake()
     {
          _instance = this;
          _playerDatabase = PlayerDatabaseFactory.Instance.Create();
     }

     public void AddScore(int score)
     {
          Score += score;
     }
}