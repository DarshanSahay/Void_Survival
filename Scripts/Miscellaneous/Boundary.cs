using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "PlayerBullet")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
