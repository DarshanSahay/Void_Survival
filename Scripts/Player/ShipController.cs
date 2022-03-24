using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class ShipController : GenericSingleton<ShipController>
{
    private ObjectPool pool;
    private float horizontal;
    private float moveSpeed;
    public float fireRate;
    public bool canShoot;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Joystick joyStick;
    [SerializeField] private Transform shootPoint1;
    [SerializeField] private Transform shootPoint2;

    private void Start()
    {
        moveSpeed = 20f;
        fireRate = 0.2f;
        pool = ObjectPool.Instance;
    }
    private void Update()
    {
        Movement();
        if (canShoot)
        {
            Shoot();
        }
    }
    public void SetShoot()
    {
        canShoot = true;
    }
    public void ResetShoot()
    {
        canShoot = false;
    }
    private void Movement()
    {
        horizontal = joyStick.Horizontal;
        Vector2 movement = transform.right * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement * horizontal);
    }
    public void Shoot()
    {
        fireRate -= Time.deltaTime;
        if (fireRate <= 0)
        {
            GameObject bullet1 = pool.GetPooledObject("PlayerBullet");
            if (bullet1 != null)
            {
                bullet1.transform.position = shootPoint1.position;
                bullet1.transform.rotation = Quaternion.identity;
                bullet1.SetActive(true);
                Rigidbody2D rb = bullet1.GetComponent<Rigidbody2D>();
                rb.velocity = transform.up * 20f;
                fireRate = 0.2f;
            }
            GameObject bullet2 = pool.GetPooledObject("PlayerBullet");
            if (bullet2 != null)
            {
                bullet2.transform.position = shootPoint2.position;
                bullet2.transform.rotation = Quaternion.identity;
                bullet2.SetActive(true);
                Rigidbody2D rb = bullet2.GetComponent<Rigidbody2D>();
                rb.velocity = transform.up * 20f;
                fireRate = 0.2f;
            }
        }
    }
}
