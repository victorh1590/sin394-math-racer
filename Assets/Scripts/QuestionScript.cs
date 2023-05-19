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
  bool? isCorrect = null;
  private GameObject spawn;
  string itemTag = null;
  int? healingAmount = null;
  private SpawnScript spawnScript = null;
  private PlayerScript playerScript = null;

  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    spawn = GameObject.FindGameObjectWithTag("Respawn");
    spawnScript = spawn.gameObject.GetComponent<SpawnScript>();
    playerScript = player.gameObject.GetComponent<PlayerScript>();
    LoadPlayerPrefs();
    DeserializeJsonFile();
  }

  // Update is called once per frame
  void Update()
  {
    // if (Time.realtimeSinceStartup > 5f && questionOpen == false && questionStack?.Count > 0)
    // {
    //   QuestionProcedure();
    // }
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
    json = json.OrderBy(_ => Guid.NewGuid()).ToList();
    questionStack = new Stack<Question>(json);
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
    currentQuestion.Answers = currentQuestion.Answers.OrderBy(_ => Guid.NewGuid()).ToList();
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
      if (chosenAnswer == i)
      {
        answers[i].text += " <Selecionado> ";
      }
    }

    return chosenAnswer != null ? currentQuestion.CorrectAnswer.Equals(currentQuestion.Answers[(int)chosenAnswer]) : false;
  }

  public IEnumerator StartQuestion(string tag, int healing)
  {
    if (questionOpen == false && questionStack?.Count > 0)
    {
      itemTag = tag;
      healingAmount = healing;
      spawnScript.StopSpawn();
      playerScript.StopUpdateFuel();
      questionOpen = true;
      isCorrect = null;
      SelectQuestion();
      Debug.Log("Is here.1");
      yield return StartCoroutine(QuestionCountdown(15, AnswerAndResolution));
    }
  }

  void AnswerAndResolution()
  {
    AnswerChosen();
    isCorrect = QuestionResolution();
  }

  void ResetParams()
  {
    questionOpen = false;
    chosenAnswer = null;
    itemTag = null;
    healingAmount = null;
  }

  void ResetQuestionUI()
  {
    statement.text = "Pergunta encerrada.";
    statement.color = Color.white;
    for (int i = 0; i < answers.Length; i++)
    {
      answers[i].text = letters[i];
      answers[i].color = Color.white;
    }
    tip.text = "Aguardando próxima pergunta.";
    tip.color = Color.white;
    timer.text = "Tempo: 00";
  }

  public void FinishQuestion()
  {
    Debug.Log("Is here.3");
    ResetQuestionUI();
    Debug.Log("Correct? " + isCorrect.ToString());
    HealLogic((bool)isCorrect!);
    ResetParams();
    spawnScript.RestartSpawn();
    playerScript.RestartUpdateFuel();
  }

  IEnumerator QuestionCountdown(int seconds, Action action)
  {
    int counter = seconds;
    while (counter > 0)
    {
      UpdateTimer(counter);
      yield return new WaitForSeconds(1);
      counter--;
    }
    action();
    yield return StartCoroutine(AnswerCountdown(5, FinishQuestion));
  }

  IEnumerator AnswerCountdown(int seconds, Action action)
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


  public void HealLogic(bool correct)
  {
    if (correct && healingAmount != null && itemTag != null)
    {
      if (itemTag == "Health") playerScript.Heal((int)healingAmount!);
      else if (itemTag == "Fuel") playerScript.AddGas((int)healingAmount!);
    }
  }
}
