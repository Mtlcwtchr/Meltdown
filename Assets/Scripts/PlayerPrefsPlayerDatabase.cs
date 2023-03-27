using UnityEngine;

public class PlayerPrefsPlayerDatabase: IPlayerDatabase
{
    private const string ScoresKey = "Player_Scores";
    
    public int GetScores() => PlayerPrefs.GetInt(ScoresKey, 0);

    public void SetScores(int scores) => PlayerPrefs.SetInt(ScoresKey, scores);
}