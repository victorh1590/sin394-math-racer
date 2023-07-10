/*using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;

public class SelectUserScript : MonoBehaviour
{
    public string NomeJogador;
    public Sprite SpriteJogador;

    public Image ImagemAtual;
    public Sprite[] SpriteCarro;
    public Button[] ButtonEsc;
    private int NumSprite = 0;

    public TMP_InputField[] Caracteres;

    public Button Confirmar;
    void Unlock()
    {
        if (Caracteres[0].text.Length > 0 && Caracteres[1].text.Length > 0 && Caracteres[2].text.Length > 0)
        {
            Confirmar.interactable = true;
        }
        else
        {
            Confirmar.interactable = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteJogador = ImagemAtual.sprite;
        Button();
        Confirmar.interactable = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        EscolhaNome();
        Unlock();
    }

    void Button()
    {
        ButtonEsc[0].onClick.AddListener(() => Escolha(0));
        ButtonEsc[1].onClick.AddListener(() => Escolha(1));
    }

    void Escolha(int var)
    {
        if (var == 0)
        {
            if (NumSprite == 0)
            {
                NumSprite = 2;
            }
            else
            {
                NumSprite--;
            }
        }else
        if (var == 1)
        {
            if (NumSprite == 2)
            {
                NumSprite = 0;
            }
            else
            {
                NumSprite++;
            }
        }
        ImagemAtual.sprite = SpriteCarro[NumSprite];

        SpriteJogador = SpriteCarro[NumSprite];
    }

    char ValidateInput(string text, int charIndex, char addedChar)
    {
        // Verifica se o caractere adicionado é uma letra
        if (!char.IsLetter(addedChar))
        {
            addedChar = '\0'; // Substitui o caractere não permitido por um caractere vazio
        }

        return addedChar;
    }

    void EscolhaNome()
    {
        for (int i = 0; i < Caracteres.Length; i++)
        {
            int currentIndex = i; // Captura o índice atual para uso no evento

            // Define o filtro personalizado para aceitar somente letras
            Caracteres[i].characterValidation = TMP_InputField.CharacterValidation.CustomValidator;
            Caracteres[i].onValidateInput += ValidateInput;

            // Adiciona um ouvinte de evento OnValueChanged para cada campo de entrada
            Caracteres[i].onValueChanged.AddListener((value) =>
            {
                // Transfere o foco para o próximo campo de entrada
                if (currentIndex < Caracteres.Length - 1 && value.Length == 1)
                    Caracteres[currentIndex + 1].Select();
                if (Input.GetKeyDown(KeyCode.Backspace) && currentIndex - 1 != -1)
                {
                    Caracteres[currentIndex - 1].Select();
                }
            });
        }
    }

    void ConcatenarNome()
    {
        StringBuilder sb = new StringBuilder();

        foreach (TMP_InputField campo in Caracteres)
        {
            sb.Append(campo.text);
        }

        NomeJogador = sb.ToString();
    }

    public class JogadorData
    {
        public string NomeJogador;
        public string SpriteJogador;
    }
    IEnumerator MenuSelect1()
    {
        ConcatenarNome();

        JogadorData jogadorData = new JogadorData
        {
            NomeJogador = NomeJogador,
            SpriteJogador = SpriteJogador.name // Salve apenas o nome do sprite por enquanto, você pode ajustar isso de acordo com suas necessidades
        };

        string json = JsonConvert.SerializeObject(jogadorData, Formatting.Indented);
        string filePath = Path.Combine(Application.persistentDataPath, "selectuser.json");

        File.WriteAllText(filePath, json);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("MenuSelect");
    }


    public void MenuSelect()
    {
        Time.timeScale = 1;
        StartCoroutine(MenuSelect1());
    }

}
*/