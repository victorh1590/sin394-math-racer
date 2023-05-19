using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractionScript : MonoBehaviour
{
  private PlayerScript player;
  public ItemScript item;
  public int healingAmount;
  //   GameObject questions;
  QuestionScript questionScript;

  // Start is called before the first frame update
  void Start()
  {
    questionScript = GameObject.FindGameObjectWithTag("QuestionManager").gameObject.GetComponent<QuestionScript>();
    // questionScript = questions.GetComponent<QuestionScript>();
    player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerScript>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnTriggerEnter2D(Collider2D obj)
  {
    // Debug.Log("collision happened");
    if (obj.tag == "Player")
    {
      StartCoroutine(questionScript.StartQuestion(this.tag, healingAmount));
    }
    this.gameObject.SetActive(false);
  }
}
