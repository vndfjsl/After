using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int bombs; //��ź ���� 
    public int firePower; //��ź�� ���� 
    public int speed; //�÷��̾� �̵��ӵ� 

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.level[(int)transform.position.x, (int)transform.position.y] = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            BombSpawner bombSpawner = collision.gameObject.GetComponent<BombSpawner>();

            playerController.speed += speed;
            bombSpawner.firePower += firePower;
            bombSpawner.numberOfBombs += bombs;

            Destroy(gameObject);
        }
    }
}
