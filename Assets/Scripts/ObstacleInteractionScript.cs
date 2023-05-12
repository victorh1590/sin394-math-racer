using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInteractionScript : MonoBehaviour
{
    private PlayerScript player;
    public ObstacleScript obstacle;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        Debug.Log("collision happened");
        if(obj.tag == "Player")
        {
            player.TakeDamage(damage);
        }
        obstacle.DestroyObstacle();
    }
}
