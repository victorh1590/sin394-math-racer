using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyShowOnMobile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!Application.isMobilePlatform) this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
