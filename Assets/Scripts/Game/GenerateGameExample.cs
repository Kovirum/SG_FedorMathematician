using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGameExample : MonoBehaviour
{
    public static List<string> GenerateExample()
    {

        List<int> exampleNums = new List<int>();
        List<string> exampleOperations = new List<string>();
        string[] gameOperations = new string[2] { "+", "-" };
        int numCount = Mathf.FloorToInt(GameController.Level / 7) + 2;
        int operationCount = numCount - 1;
        for (int i = 0; i < numCount; i++)
        {
            exampleNums.Add(Random.Range(-10, 10 + 1));
        }
        for (int i = 0; i < operationCount; i++)
        {
            exampleOperations.Add(gameOperations[Random.Range(0, 1 + 1)]);
        }

        List<string> example = new List<string>();

        for (int i = 0; i < numCount; i++)
        {
            example.Add(exampleNums[i].ToString());

            if (i < operationCount)
            {
                example.Add(exampleOperations[i]);
            }
        }
        return example;
    }

    public static string ConvertExampleToDefaultView(List<string> example)
    {
        return string.Join(" ", example).Replace(" ", "").Replace("--", "+").Replace("+-", "-").Replace("-+", "-");
    }

    public static int MathExampleResult(List<string> example)
    {
        int result = 0;

        for (int i = 0; i < example.Count; i++)
        {
            if (int.TryParse(example[i], out int number))
            {
                if (i + 2 < example.Count)
                {
                    if (example[i + 1] == "+")
                    {
                        result = number + int.Parse(example[i + 2]);
                    }
                    else
                    {
                        result = number - int.Parse(example[i + 2]);
                    }
                    example[i + 2] = result.ToString();
                }
            }
        }
        return result;
    }

    public static List<int> GenerateRandomWrongAnswers(int correctAnswer)
    {
        List<int> answers = new List<int>
        {
            correctAnswer,
        };

        while (answers.Count < 3)
        {
            int randomAnswer = Random.Range(-10, 10 + 1);

            if (randomAnswer != correctAnswer && !answers.Contains(randomAnswer))
            {
                answers.Add(randomAnswer);
            }
        }

        for (int i = 0; i < answers.Count; i++)
        {
            int temp = answers[i];
            int randomIndex = Random.Range(i, answers.Count);
            answers[i] = answers[randomIndex];
            answers[randomIndex] = temp;
        }

        return answers;
    }
}
