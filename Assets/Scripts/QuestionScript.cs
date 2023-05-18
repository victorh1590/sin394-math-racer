using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameModels;
using System.IO;
using TMPro;
using System.Linq;
using Newtonsoft.Json;
using System;

public class QuestionScript : MonoBehaviour
{
  //   List<int> roadCoordsY = new() { 3, 2, 1, 0 };
  List<string> letters = new() { "a)", "b)", "c)", "d)" };
  GameObject player;
  Stack<Question> questionStack = new();
  bool questionOpen = false;
  public TextMeshProUGUI statement;
  public TextMeshProUGUI[] answers = new TextMeshProUGUI[4];
  public TextMeshProUGUI tip;
  public TextMeshProUGUI timer;
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
    if (Time.realtimeSinceStartup > 5f && questionOpen == false && questionStack.Count > 0)
    {
      QuestionProcedure();
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
    var guid = Guid.NewGuid();
    var json = JsonConvert.DeserializeObject<List<Question>>(PlayerPrefs.GetString("original_questions"));
    json = json.OrderBy(_ => guid).ToList();
    json.ForEach(question => question.Answers = question.Answers.OrderBy(_ => guid).ToList());
    questionStack = new Stack<Question>(json);
    // questionCount = questionList.Count;
  }

  void AnswerChosen()
  {
    var positionY = player.transform.position.y;
    if (positionY >= -1 && positionY < 0)
    {
      chosenAnswer = 3;
    }
    else if (positionY >= 0 && positionY < 1)
    {
      chosenAnswer = 2;
    }
    else if (positionY >= 1 && positionY < 2)
    {
      chosenAnswer = 1;
    }
    else if (positionY >= 2 && positionY < 3)
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
    // questionCount--;
    if (questionStack.Count <= 0) throw new Exception("Question stack is empty.");
    currentQuestion = questionStack.Pop();
    // currentQuestion.Answers = currentQuestion.Answers.OrderBy(_ => Guid.NewGuid()).ToList();
    statement.text = "Pergunta: " + currentQuestion.Statement;
    statement.color = Color.white;
    for (int i = 0; i < answers.Length; i++)
    {
      answers[i].text = letters[i] + " " + currentQuestion.Answers[i];
      answers[i].color = Color.white;
    }
    tip.text = "Dica: " + currentQuestion.Tip;
    tip.color = Color.white;
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

  private void QuestionProcedure()
  {
    questionOpen = true;
    SelectQuestion();
    StartCoroutine(Countdown(15, AnswerAndResolution));
  }

  void AnswerAndResolution()
  {
    AnswerChosen();
    bool correct = QuestionResolution();
    StartCoroutine(Countdown(5, ResetQuestionParams));
  }

  void ResetQuestionParams()
  {
    questionOpen = false;
    chosenAnswer = null;
  }

  IEnumerator Countdown(int seconds, Action action)
  {
    int counter = seconds;
    while (counter > 0)
    {
      UpdateTimer(counter);
      yield return new WaitForSeconds(1);
      counter--;
    }
    action();
  }

  void UpdateTimer(int counter)
  {
    timer.text = "Tempo: " + counter.ToString("00");
  }
}
