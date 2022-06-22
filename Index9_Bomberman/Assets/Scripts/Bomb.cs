using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bomb : NetworkBehaviour
{
    public GameObject fire;
    public int firePower;
    public float fuse;
    Vector3[] directions = new Vector3[] {Vector3.up, Vector3.down, Vector3.left, Vector3.right};
    GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        if(IsServer) Invoke("Explode", fuse);
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }
    
    public void Explode()
    {
        if (!IsServer) return;

        Debug.Log("BOOM:"+firePower);
        //for center
        GameObject g = Instantiate(fire, transform.position, Quaternion.identity);
        g.GetComponent<NetworkObject>().Spawn();
        foreach(var dir in directions)
        {
            SpawnFire(dir);
        }

        Destroy(gameObject);
    }

    void SpawnFire(Vector3 offset, int fire = 1)
    {
        int x = (int)transform.position.x + (int)offset.x * fire;
        int y = (int)transform.position.y + (int)offset.y * fire;
        Debug.Log("spawn fire:" +x+"/"+y);

        if (gc.level[x,y] == null && fire < firePower)
        {
            GameObject f = Instantiate(this.fire, transform.position + (offset*fire), Quaternion.identity);
            f.GetComponent<NetworkObject>().Spawn();
            SpawnFire(offset, ++fire);
        }
        else if(fire < firePower)
        {
            //지형물 파이어 
            if (gc.level[x,y] != null && gc.level[x,y].tag == "Destroyble")
            {
                GameObject f = Instantiate(this.fire, transform.position + (offset*fire), Quaternion.identity);
                f.GetComponent<NetworkObject>().Spawn();
            }
        }
        
    }
}
