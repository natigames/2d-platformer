using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth.instance.DealDamage();
            LevelManager.instance.RespawnPlayer();
        }
    }
}
