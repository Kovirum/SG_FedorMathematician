using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateResult : MonoBehaviour
{
    public Text ResultText;

    private void Awake()
    {
        ResultText.text = $"Результат - {GameController.Scores}";
        LeaderboardManager.Add(PlayerPrefs.GetString("Nickname", "anonymous"), GameController.Scores);
    }
}
