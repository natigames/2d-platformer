using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public float waitToRespawn;
    public int gemsCollected;

    public string levelToLoad;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        // kill, wait and respawn  (transition screens with fade in/out)
        Player.instance.gameObject.SetActive(false);
        AudioManager.instance.PlaySFX(8);

        yield return new WaitForSeconds(waitToRespawn - (1f/UIController.instance.fadeSpeed));
        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f);
        UIController.instance.FadeFromBlack();

        Player.instance.gameObject.SetActive(true);
        Player.instance.transform.position = CheckpointController.instance.spawnPoint;

        PlayerHealth.instance.currentHealth = PlayerHealth.instance.maxhealth;
        UIController.instance.UpdateHealthDisplay();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }


    public IEnumerator EndLevelCo()
    {
        Player.instance.stopInput = true;
        Camera.instance.stopFollow = true;
        UIController.instance.levelCompleteText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + 0.25f);
        SceneManager.LoadScene(levelToLoad);
    }

}
