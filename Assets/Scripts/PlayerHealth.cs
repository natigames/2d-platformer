using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    //so that it becomes available from other scripts!
    public static PlayerHealth instance; // create a _self global variable for singleton (awake)

    public int currentHealth, maxhealth;

    public float invincibleLength; //allowed duration
    private float invincibleCounter; //count down whenever get hit

    private SpriteRenderer theSR;


    private void Awake()
    {
        instance = this; // this helps reference the global variable for singleton
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            //this will only happen once (to reset Alpha)
            if (invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }
    }

    public void DealDamage()
    {
        if(invincibleCounter <= 0)
        { 
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0; //prevent weird errors if <0

                //gameObject.SetActive(false);
                //now control with level manager
                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                //blink with alpha to show damage
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 0.5f);

                Player.instance.KnockBack(); //call player singleton
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }


}
