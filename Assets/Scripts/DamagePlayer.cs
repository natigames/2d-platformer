using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
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
            //hit
            //FindObjectOfType<PlayerHealth>().DealDamage();
            PlayerHealth.instance.DealDamage(); // better approach with Singleton

            AudioManager.instance.PlaySFX(9);
        }
    }

}
