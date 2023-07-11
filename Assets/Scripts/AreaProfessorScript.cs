using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using GameModels;
using System;
using TMPro;

public class AreaProfessorScript : MonoBehaviour
{
    private string fileName = "CUSTOM_QUESTIONS.json";
    private string path = "";
    private string content = "";
    private List<Question> questionList = new();
    public TMP_InputField alternativaA;
    public TMP_InputField alternativaB;
    public TMP_InputField alternativaC;
    public TMP_InputField alternativaD;
    public TMP_InputField pergunta;
    public TMP_InputField dica;
    public GameObject failPanel;
    public GameObject sucessPanel;
    public GameObject resetPanel;
    public TextMeshProUGUI numQuestions;
    // Start is called before the first frame update
    void Start()
    {
        path = Path.Combine(Application.streamingAssetsPath, "Resources", fileName);
        CreateFile();
        DeserializeJsonFile();
        UpdateNumQuestions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        string newJson = JsonConvert.SerializeObject(questionList, Formatting.Indented);
        File.WriteAllText(path, newJson);
    }

    void UpdateNumQuestions()
    {
        numQuestions.text = questionList.Count + "/50";
    }

    public void AddQuestion()
    {
        if(ValidateQuestion())
        {
            questionList.Add(new Question 
            { 
                Statement = pergunta.text,
                Answers = new List<string> 
                { 
                    alternativaA.text, 
                    alternativaB.text, 
                    alternativaC.text, 
                    alternativaD.text
                },
                CorrectAnswer = alternativaA.text,
                Tip = dica.text
            });
            UpdateNumQuestions();
            ShowSucessPopup();
        }
        else
        {
            ShowFailPopup();
        }
    }

    void ResetInputFields()
    {
        pergunta.text = "";
        alternativaA.text = "";
        alternativaB.text = "";
        alternativaC.text = "";
        alternativaD.text = "";
        dica.text = "";
    }

    public void ShowFailPopup()
    {
        failPanel.SetActive(true);
    }

    public void ShowSucessPopup()
    {
        sucessPanel.SetActive(true);
    }

    public void ShowResetPopup()
    {
        resetPanel.SetActive(true);
    }

    public void ClosePoup()
    {
        ResetInputFields();
        resetPanel.SetActive(false);
        failPanel.SetActive(false);
        sucessPanel.SetActive(false);
    }

    bool ValidateQuestion() => 
        pergunta.text.Length > 1 && 
        alternativaA.text.Length > 1 &&
        alternativaB.text.Length > 1 &&
        alternativaC.text.Length > 1 &&
        alternativaD.text.Length > 1 &&
        dica.text.Length > 1 &&
        pergunta.text.Length <= 150 && 
        alternativaA.text.Length <= 30 &&
        alternativaB.text.Length <= 30 &&
        alternativaC.text.Length <= 30 &&
        alternativaD.text.Length <= 30 &&
        dica.text.Length <= 100;

    public void ResetQuestions()
    {
        questionList = new List<Question>();
        UpdateNumQuestions();
        ShowResetPopup();
    }

    IEnumerator CreateFile()
    {
        yield return new WaitForSeconds(1);
        if (!File.Exists(path))
        {
            File.Create(path);
        }
        content = File.ReadAllText(path, System.Text.Encoding.UTF8);
        // Debug.Log(path);
        // Debug.Log(content);
    }
    void DeserializeJsonFile()
    {
        if(!String.IsNullOrEmpty(content))
        {
            var json = JsonConvert.DeserializeObject<List<Question>>(content);
            questionList = new List<Question>(json);
        }
    }
}
