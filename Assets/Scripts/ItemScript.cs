using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
  ScrollingBackgroundScript bgScript;

  [SerializeField]
  GameObject background;

  [SerializeField]
  Renderer bgRenderer;
  [SerializeField]
  Transform bgTransform;

  // Start is called before the first frame update
  void Start()
  {
    bgScript = GameObject.Find("Background").GetComponent<ScrollingBackgroundScript>();
    bgRenderer = GameObject.Find("Background").GetComponent<Renderer>();
    bgTransform = GameObject.Find("Background").GetComponent<Transform>();
  }

  // Update is called once per frame
  void Update()
  {
    Movement();
  }

  private void Movement()
  {
    transform.Translate(Vector3.left * bgTransform.localScale.x * bgScript.speed * Time.deltaTime);
    if (transform.position.x < -10f) DestroyItem();
  }

  public void DestroyItem() => Destroy(this.gameObject);
}
