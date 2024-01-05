using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateResult : MonoBehaviour
{
    public Text ResultText;

    private void Awake()
    {
        ResultText.text = $"��������� - {GameController.Scores}";
        LeaderboardManager.Add(PlayerPrefs.GetString("Nickname", "anonymous"), GameController.Scores);
    }
}
