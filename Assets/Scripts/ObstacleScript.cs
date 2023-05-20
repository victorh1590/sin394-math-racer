using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
  ScrollingBackgroundScript bgScript;
  ScrollingBackgroundScript roadScript;

  [SerializeField]
  GameObject background;

  [SerializeField]
  Renderer bgRenderer;
  [SerializeField]
  Transform bgTransform;
  public float moveOffset = 0.1329f;
  public float moveFactor = 1f;


  // Start is called before the first frame update
  void Start()
  {
    bgScript = GameObject.Find("Background").GetComponent<ScrollingBackgroundScript>();
    bgRenderer = GameObject.Find("Background").GetComponent<Renderer>();
    bgTransform = GameObject.Find("Background").GetComponent<Transform>();
    roadScript = GameObject.Find("Pista1").GetComponent<ScrollingBackgroundScript>();
  }

  // Update is called once per frame
  void Update()
  {
    Movement();
  }

  private void Movement()
  {
    transform.Translate(Vector3.left * bgTransform.localScale.x * (roadScript.speed - (moveFactor * moveOffset)) * Time.deltaTime);
    if (transform.position.x < -10f) DestroyObstacle();
  }

  public void DestroyObstacle() => Destroy(this.gameObject);
}
