using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractionScript : MonoBehaviour
{
    private PlayerScript player;
    public ItemScript item;
    public int healingAmount;

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
        // Debug.Log("collision happened");
        if(obj.tag == "Player")
        {
            if(this.tag == "Health") player.Heal(healingAmount);
            else if (this.tag == "Fuel") player.AddGas(healingAmount);
        }
        item.DestroyItem();
    }
}
