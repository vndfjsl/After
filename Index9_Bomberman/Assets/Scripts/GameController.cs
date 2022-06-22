using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameController : NetworkBehaviour
{
    public const int X = 22;
    public const int Y = 13;

    public GameObject levelHolder;
    public GameObject[,] level = new GameObject[X, Y]; //grid


    // Start is called before the first frame update
    void Start()
    {
        var levels = levelHolder.GetComponentsInChildren<Transform>();
        foreach(var child in levels)
        {
            level[(int)child.position.x,(int)child.position.y] = child.gameObject;
        }
    }

    public void LevelScan()
    {
        if (!IsServer) return;

        var lhg = Instantiate(levelHolder, Vector3.zero, Quaternion.identity);
        lhg.GetComponent<NetworkObject>().Spawn();

        var levels = lhg.GetComponentsInChildren<Transform>();

        foreach (var child in levels)
        {
            level[(int)child.position.x, (int)child.position.y] = child.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
