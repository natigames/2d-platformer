using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController instance;      

    private Checkpoint[] checkpoints;
    public Vector3 spawnPoint;


    private void Start()
    {
        // get all checkpoints!
        checkpoints = FindObjectsOfType<Checkpoint>();
        spawnPoint = Player.instance.transform.position;
    }

    private void Awake()
    {
        instance = this;
    }

    public void DeactivateCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }
       
   public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

}
