using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public string type;
    Rigidbody2D rigid;
    int ranX, ranY;

    void Awake()
    {
        ranX = Random.Range(5, -5);
        ranY = Random.Range(5, -5);
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(ranX, ranY);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    rigid.velocity = new Vector2(ranX, -ranY);
                    break;
                case "Bottom":
                    rigid.velocity = new Vector2(ranX, -ranY);
                    break;
                case "Right":
                    rigid.velocity = new Vector2(-ranX, ranY);
                    break;
                case "Left":
                    rigid.velocity = new Vector2(-ranX, ranY);
                    break;
            }
        }
    }
   }
