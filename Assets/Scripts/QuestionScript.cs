using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameModels;
using System.IO;
using TMPro;
using System.Linq;
using Newtonsoft.Json;

public class QuestionScript : MonoBehaviour
{
  List<int> roadCoordsY = new() { 3, 2, 1, 0 };
  List<string> letters = new() { "a)", "b)", "c)", "d)" };
  GameObject player;
  List<Question> questionList = new();
  bool questionOpen = false;
  public TextMeshProUGUI statement;
  public TextMeshProUGUI[] answers = new TextMeshProUGUI[4];
  public TextMeshProUGUI tip;
  byte? chosenAnswer;
  Question currentQuestion = new();



  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    LoadPlayerPrefs();
    DeserializeJsonFile();
  }

  // Update is called once per frame
  void Update()
  {
    if (Time.realtimeSinceStartup > 5f)
    {
      StartCoroutine(QuestionProcedure());
    }
  }

  void LoadPlayerPrefs()
  {
    var path = Path.Combine(Application.dataPath, "Resources", "questions.json");
    var content = File.ReadAllText(path, System.Text.Encoding.UTF8);
    Debug.Log(path);
    Debug.Log(content);
    PlayerPrefs.SetString("original_questions", content);
    PlayerPrefs.Save();
  }

  void DeserializeJsonFile()
  {
    var json = JsonConvert.DeserializeObject<List<Question>>(PlayerPrefs.GetString("original_questions"));
    questionList = new List<Question>(json);
    Debug.Log(PlayerPrefs.GetString("original_questions"));
  }

  void AnswerChosen()
  {
    var positionY = player.transform.position.y;
    // positionY = (int)Mathf.Floor(positionY);
    // for (byte i = 0; i < roadCoordsY.Count; i++)
    // {
    //   if (positionY == roadCoordsY[i])
    //   {
    //     chosenAnswer = i;
    //     break;
    //   }
    // }


    if(positionY >= -1 && positionY < 0)
    {
        chosenAnswer = 3;
    }
    else if(positionY >= 0 && positionY < 1)
    {
        chosenAnswer = 2;
    }
    else if(positionY >= 1 && positionY < 2)
    {
        chosenAnswer = 1;
    }
    else if(positionY >= 2 && positionY < 3)
    {
        chosenAnswer = 0;
    }
    else 
    {
        chosenAnswer = null;
    }
    Debug.Log(chosenAnswer);
  }

  void SelectQuestion()
  {
    currentQuestion = questionList[Random.Range(0, questionList.Count)];
    statement.text = "Pergunta: " +currentQuestion.Statement;
    for (int i = 0; i < answers.Length; i++)
    {
      answers[i].text = letters[i] + " " + currentQuestion.Answers[i];
    }
    tip.text = "Dica: " + currentQuestion.Tip;
  }

  bool QuestionResolution()
  {
    for (int i = 0; i < answers.Length; i++)
    {
      if (currentQuestion.Answers[i].Equals(currentQuestion.CorrectAnswer))
      {
        answers[i].color = Color.green;
      }
      else
      {
        answers[i].color = Color.red;
      }
    }

    return chosenAnswer != null ? currentQuestion.CorrectAnswer.Equals(currentQuestion.Answers[(int)chosenAnswer]) : false;
  }

  private IEnumerator QuestionProcedure()
  {
    questionOpen = true;
    SelectQuestion();
    yield return new WaitForSeconds(15);
    AnswerChosen();
    bool correct = QuestionResolution();
    yield return new WaitForSeconds(5);
    questionOpen = false;
    chosenAnswer = null;
    // currentQuestion = new();
  }
}
