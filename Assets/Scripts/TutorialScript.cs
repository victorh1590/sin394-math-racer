using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{


    public GameObject SetaHudVida;
    public GameObject SetaHudTimer;
    public GameObject SetaHudPontos;

    public GameObject HudVida;
    public GameObject HudTimer;
    public GameObject HudPontos;

    public GameObject painel;
    public GameObject[] Cientist;
    public GameObject wasdPanel;
    public GameObject espaco;
    public GameObject EspaçoA;
    public TextMeshProUGUI texto;

    private List<string> dialogo = new List<string>();
    public int indiceFala = 0;

    private bool apertada1 = false, apertada2 = false, apertada3 = false, apertada4 = false;
    private bool skip = false;
    private bool block = false;
    private float cooldownTime = 3f;
    private float lastSkipTime = 0f;
    private float cooldownTimeA = 1f;
    private float lastSkipTimeA = 0f;
    private int contador = 0;

    private bool Ativado = true;
    private bool[] setaactive = { false, false, false };

    private void Start()
    {
        falasAdd();
        startTutorial();
    }

    private void Update()
    {
        Falas();
        coletarSkip();
        animacaoTecla();
        coletarTeclasWASD();
    }
    private void animacaoTecla()
    {
        if (Time.time > lastSkipTimeA + cooldownTimeA)
        {
            EspaçoA.SetActive(Ativado);
            if (setaactive[0])
            {
                SetaHudVida.SetActive(Ativado);
            }
            else if (setaactive[1])
            {
                SetaHudTimer.SetActive(Ativado);
            }
            else if (setaactive[2])
            {
                SetaHudPontos.SetActive(Ativado);
            }
            Ativado = !Ativado;
            lastSkipTimeA = Time.time;
        }
    }
    private void falasAdd()
    {
       /*0*/ dialogo.Add("Olá! Seja bem vindo ao tutorial.\nMeu nome é Dr. Heinz Doof, fui escalado para lhe guiar.");
       /*1*/ dialogo.Add("Para movimentar, aperte as teclas a seguir:");
       /*2*/ dialogo.Add("Esse aqui é o seu painel de status, aqui será indicado a sua vida e a quantidade de gasolina que restam em seu carro.");
       /*3*/ dialogo.Add("Aqui é tempo de duração da fase, o objetivo é conseguir alcançar o tempo de 2 Minutos sem que a sua vida e gasolina zere.");
       /*4*/ dialogo.Add("Esse  é a sua pontuação da fase. Ela irá aumentar ao longo do tempo, e também sera adiconado pontos por acertos das questões que surgirem.");

    }
    private IEnumerator FalasCoroutine()
    {
        if (indiceFala < dialogo.Count)
        {
            texto.text = dialogo[indiceFala];
            if(indiceFala == 0)
            {
                Time.timeScale = 0f;
                Cientist[1].SetActive(false);
            }


            if (indiceFala < dialogo.Count && skip == true)
            {
                skip = false;
                indiceFala++;
                if (indiceFala == 1)
                {
                    block = true;
                    wasdPanel.SetActive(true);
                    espaco.SetActive(false);
                }
                else
                {
                    wasdPanel.SetActive(false);
                }
                if (indiceFala == 2)
                {
                    Time.timeScale = 1f;
                    yield return new WaitForSeconds(2f); // Atraso de 2 segundos
                    Time.timeScale = 0f;
                    painel.SetActive(true);
                    espaco.SetActive(true);
                    HudVida.SetActive(true);
                    setaactive[0] = true;
                }
                else if (indiceFala == 3)
                {
                    SetaHudVida.SetActive(false);
                    Cientist[0].SetActive(false);
                    Cientist[1].SetActive(true);

                    setaactive[0] = false;
                    setaactive[1] = true;
                    HudTimer.SetActive(true);
                }
                else if (indiceFala == 4)
                {
                    SetaHudTimer.SetActive(false);
                    setaactive[1] = false;
                    setaactive[2] = true;
                    HudPontos.SetActive(true);
                }
                else if (indiceFala == 5)
                {
                    SetaHudPontos.SetActive(false);
                    setaactive[2] = false;
                }
            }
        }
    }

    private void Falas()
    {
        StartCoroutine(FalasCoroutine());
    }
    private void coletarSkip()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > lastSkipTime + cooldownTime && painel.activeSelf && !block)
        {
            skip = true;
            lastSkipTime = Time.time;
        }
    }
/*                    StartCoroutine(DelayTimer(7));
*/    private void coletarTeclasWASD()
    {        
        if (apertada1 == false && Input.GetKey(KeyCode.W) && indiceFala >= 1)
        {
            apertada1 = true;
            contador++;
        }
        else if (apertada2 == false && Input.GetKey(KeyCode.S) && indiceFala >= 1) 
        {
            apertada2 = true;
            contador++;
        }
        else if (apertada3 == false && Input.GetKey(KeyCode.A) && indiceFala >= 1)
        {
            apertada3 = true;
            contador++;
        }
        else if (apertada4 == false && Input.GetKey(KeyCode.D) && indiceFala >= 1)
        {
            apertada4 = true;
            contador++;
        }

        if(contador == 4)
        {
            painel.SetActive(false);
            block = false;
            skip = true;
            contador = 0;
        }
    }
    private void startTutorial()
    {
        Falas();
        painel.SetActive(true);
    }
    /*

    private void OpenPanel()
    {
        wasdPanel.SetActive(true);
        StartCoroutine(PannelTimer());
    }

    private IEnumerator PannelTimer()
    {
*//*        yield return new WaitForSeconds(15); *//*       
        wasdPanel.SetActive(false);
    }*/
}
