using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour,IDamageable
{
    private UIManager ui;
    private int currentHealth;
    private int maxHealth;

    private void Start()
    {
        ui = UIManager.Instance;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)                        //taking damage and setting the collided object with ship to inactive
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            collision.gameObject.SetActive(false);
            ScoreManager.Instance.collectedPowerUp = true;
        }
        else if (collision.gameObject.tag == "EnemyBullet")
        {
            collision.gameObject.SetActive(false);
            IDamageable damageable = GetComponent<IDamageable>();
            damageable.TakeDamage(10);
            UpdateHealth();
        }
        if (collision.gameObject.tag == "Asteroid1" ||
            collision.gameObject.tag == "Asteroid2")
        {
            collision.gameObject.SetActive(false);
            IDamageable damageable = GetComponent<IDamageable>();
            damageable.TakeDamage(10);
            UpdateHealth();
        }
        if (collision.gameObject.GetComponent<EnemyConroller>() != null)
        {
            collision.gameObject.SetActive(false);
            IDamageable damageable = GetComponent<IDamageable>();
            damageable.TakeDamage(15);
            UpdateHealth();
        }
    }
    public void SetHealth()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        UpdateHealth();
    }
    private void UpdateHealth()
    {
        ui.playerHealthBar.value = currentHealth;

        if(currentHealth <= 0)
        {
            GameManager.Instance.SetGameState(GameStates.GameEnded);
        }
    }
}
