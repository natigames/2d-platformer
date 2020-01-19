using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public static PlayerHealth instance; // create a _self global variable for singleton (awake)

    public int currentHealth, maxhealth;

    private void Awake()
    {
        instance = this; // this helps reference the global variable for singleton
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);

        }
    }

}
