using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PowerUpController : NetworkBehaviour
{
    public void SpawnPowerUP(Vector3 position, GameObject powerUp)
    {
        if (!IsServer) return;

        GameObject g = Instantiate(powerUp, position, Quaternion.identity );
        g.GetComponent<NetworkObject>().Spawn();
    }
}