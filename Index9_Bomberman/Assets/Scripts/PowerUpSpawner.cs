using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{   //Box
    public GameObject[] powerUps; //items
    PowerUpController powerUpController;

    // Start is called before the first frame update
    void Start()
    {
        powerUpController = FindObjectOfType<PowerUpController>();
    }
    
    public void SpawnPowerUp()
    {
        int randomIndex = Random.Range(0, powerUps.Length);
        if (Random.Range(0f,1f) > 0.5f)
        {
            Debug.Log(powerUps);
            powerUpController.SpawnPowerUP(transform.position, powerUps[randomIndex]);  //item generate.
        }
    }
}
