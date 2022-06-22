using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    public Vector3[] spawnPositions;
    public static int numberOfPlayers;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GameObject.Find("GameController").GetComponent<GameController>().LevelScan();
        int playerIndex = numberOfPlayers;
        GetComponent<SpriteRenderer>().sprite = sprites[playerIndex];
        transform.position = spawnPositions[playerIndex];
        numberOfPlayers++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer) return;

        float x = Input.GetAxisRaw("Horizontal"); 
        float y = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(x) >= Mathf.Abs(y))
        {
            y = 0;

        }else if (Mathf.Abs(y) >= Mathf.Abs(x))
        {
            x = 0;
        }

        PositionServerRpc(x,y);
    }

    [ServerRpc]
    private void PositionServerRpc(float x, float y)
    {
        Vector2 movement = new Vector2(x, y) * speed;
        rb2d.velocity = movement;
    }
}
