using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool IsGameStopped = false;
    public static int Level = 1;
    public static int Scores = 0;
    public static float TimeRemain = 60f;

    private int _correctAnswer;
    private float _endGameTimer = 3f;


    public Animator Animator;

    public Text TimeRemainText;
    public Text LevelText;
    public Text ScoresText;
    public Text ExampleText;

    public Text[] Options = new Text[3];


    public void Awake()
    {
        IsGameStopped = false;
        Level = 1;
        Scores = 0;
        TimeRemain = 60f;

        NewGame();
    }

    public void Update()
    {
        if (!IsGameStopped)
        {
            TimeRemain -= Time.deltaTime;
            TimeRemain = TimeRemain < 0 ? 0 : TimeRemain;
            TimeRemainText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(TimeRemain / 60), Mathf.FloorToInt(TimeRemain % 60));
            if (TimeRemain <= 0f)
            {
                EndGame();
            }
        }
        else
        {
            if (TimeRemain <= 0f)
            {
                _endGameTimer -= Time.deltaTime;
                if (_endGameTimer <= 0)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    public void NewGame()
    {
        List<string> example = GenerateGameExample.GenerateExample();
        ExampleText.text = GenerateGameExample.ConvertExampleToDefaultView(example);
        LevelText.text = $"Уровень\n{Level}";
        ScoresText.text = $"Очки\n{Scores}";
        _correctAnswer = GenerateGameExample.MathExampleResult(example);
        List<int> allAnswers = GenerateGameExample.GenerateRandomWrongAnswers(_correctAnswer);

        for (int i = 0; i < Options.Length; i++)
        {
            Options[i].text = allAnswers[i].ToString();
        }
    }

    public void CheckAnswer(int buttonID)
    {
        if (!IsGameStopped)
        {
            int selectedAnswer = int.Parse(Options[buttonID].GetComponentInChildren<Text>().text);

            if (selectedAnswer == _correctAnswer)
            {
                Animator.Play("RightAnswer", 0, 0);
                Scores += Mathf.FloorToInt(Level / 10) * 2 + 5;
                int addTimeSum = 5 - Mathf.FloorToInt(Level / 10);
                addTimeSum = addTimeSum < 0 ? 0 : addTimeSum;
                TimeRemain += addTimeSum;
                TimeRemain = TimeRemain > 60 ? 60 : TimeRemain;
                Level++;
            }
            else
            {
                TimeRemain -= 7;
                //gameObject.GetComponent<AnimationController>().PlayAnim("WrongAnswer");
                Animator.Play("WrongAnswer", 0, 0);
            }
            NewGame();
        }
    }

    public void EndGame()
    {
        IsGameStopped = true;
        for (int i = 0; i < Options.Length; i++)
        {
            Options[i].text = "";
        }
        ExampleText.text = "Game over";
    }
}
