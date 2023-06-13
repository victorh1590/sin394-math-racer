using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    private PlayerScript PScript;

    public GameObject SetaHudVida;
    public GameObject SetaHudTimer;
    public GameObject SetaHudPontos;

    public GameObject HudVida;
    public GameObject HudTimer;
    public GameObject HudPontos;

    public GameObject Obst;
    public GameObject PU;

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
    private bool[] setaactive = { false, false, false, false, false};
    private void Start()
    {
        PScript = FindObjectOfType<PlayerScript>();
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
            else if (setaactive[3])
            {
                Obst.SetActive(Ativado);
            }
            else if (setaactive[4])
            {
                PU.SetActive(Ativado);
            }
            Ativado = !Ativado;
            lastSkipTimeA = Time.time;
        }
    }
    private void falasAdd()
    {
       /*0*/ dialogo.Add("Olá! Seja bem-vindo ao tutorial. Meu nome é Dr. Alex e fui designado para ser seu guia.");
       /*1*/ dialogo.Add("Para se movimentar, utilize as \"WASD\"");
       /*2*/ dialogo.Add("Este é o seu painel de status, onde você pode acompanhar a sua vida e a quantidade de gasolina restante no seu carro.");
       /*3*/ dialogo.Add("Aqui está o tempo de duração da fase. O objetivo é conseguir alcançar 2 minutos sem que sua vida e gasolina se esgotem.\"");
       /*4*/ dialogo.Add("A pontuação da fase representa o seu desempenho ao longo do tempo e será aumentada gradualmente. Além disso, você também receberá pontos adicionais por acertar as questões que surgirem durante o jogo.");
       /*5*/ dialogo.Add("Durante cada fase, obstáculos aparecerão no caminho. Se você colidir com esses obstáculos, sua vida será reduzida. Portanto, é importante tomar cuidado e evitar colisões.");
       /*6*/ dialogo.Add("Se você perder um coração e/ou estiver com pouca gasolina, serão exibidos \"Power Ups\" coletaveis para ajudá-lo a recuperar o que foi perdido ao longo da corrida.");
       /*7*/ dialogo.Add("Ao coletar um item, você será desafiado por um teste exibido nesta área de diálogo. Para obter o item, é necessário responder corretamente. Caso responda incorretamente, você perderá o \"Power Up\".");

    }
    private IEnumerator FalasCoroutine()
    {
        if (indiceFala < dialogo.Count)
        {
            texto.text = dialogo[indiceFala];
            if(indiceFala == 0)
            {
                PScript.PPP(false);
                Cientist[1].SetActive(false);
            }


            if (indiceFala < dialogo.Count && skip == true)
            {
                skip = false;
                indiceFala++;
                if (indiceFala == 1)
                {
                    PScript.PPP(true);
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

                    yield return new WaitForSeconds(2f); // Atraso de 2 segundos
                    PScript.PPP(false);
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
                    Obst.SetActive(true);
                    setaactive[2] = false;
                }
                else if (indiceFala == 6)
                {
                    Obst.SetActive(false);
                    PU.SetActive(true);
                    setaactive[3] = false;
                }
                else if (indiceFala == 7)
                {
                    PU.SetActive(false);
                    setaactive[4] = false;
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
