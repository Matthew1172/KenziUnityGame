using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    public GameObject platformDestructionPoint;

    void Start()
    {
        platformDestructionPoint = GameObject.Find("platformDestructionPoint");        
    }
    
    void Update()
    {
        if(transform.position.x < platformDestructionPoint.transform.position.x)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
