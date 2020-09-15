using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NoBulletZone")
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            gameObject.SetActive(false);
        }
    }

}
