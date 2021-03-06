using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    KamiKaze,
    Fighter
}
public class EnemyConroller : MonoBehaviour,IDamageable
{
    public EnemyType type;
    private ObjectPool pool;
    private ShipController player;
    private Rigidbody2D rb;
    [SerializeField] private PolygonCollider2D col;
    private int currentHealth;
    private int maxHealth;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private Image healthBar;

    private void OnEnable()
    {
        SetStats();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * 10f;
        if (type == EnemyType.Fighter)
        {
            Invoke(nameof(ShootBullet), 2f);
        }
    }
    void Start()
    {
        player = ShipController.Instance;
        pool = ObjectPool.Instance;
    }
    private void SetStats() 
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
        col.isTrigger = true;
    }
    private void ShootBullet()
    {
        GameObject bullet = pool.GetPooledObject("EnemyBullet");
        if (bullet != null)
        {
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);

            Vector2 direction = player.transform.position - bullet.transform.position;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction;
            Invoke(nameof(ShootBullet), 2f);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)        //after colliding with player bullet , take damage and activate impact effect
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            collision.gameObject.SetActive(false);
            IDamageable damageable = GetComponent<IDamageable>();
            damageable.TakeDamage(15);

            GameObject impact = pool.GetPooledObject("ImpactParticle");
            if(impact != null)
            {
                impact.transform.position = transform.position;
                impact.transform.rotation = Quaternion.identity;
                impact.SetActive(true);
            }
            StartCoroutine(InActiveObject(impact,0.07f));
            UpdateHealth();
        }
        if(collision.gameObject.tag == "Boundary")
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator InActiveObject(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.gameObject.SetActive(false);
    }
    private void UpdateHealth()                                             //update health when taken damage and activate destroyed effect on health is zero
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
        if(currentHealth <= 0)
        {
            col.isTrigger = false;
            rb.position = transform.position;
            ScoreManager.Instance.AddScore();
            StartCoroutine(InActiveObject(gameObject, 0.1f));
            GameObject effect = pool.GetPooledObject("DestroyEffect");
            if (effect != null)
            {
                effect.transform.position = transform.position;
                effect.transform.rotation = Quaternion.identity;
                effect.SetActive(true);
            }
            StartCoroutine(InActiveObject(effect, 0.02f));
        }
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
