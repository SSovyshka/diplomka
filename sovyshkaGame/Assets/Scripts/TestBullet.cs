using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public int destroy = 5;
    public TesstWeapons weapon;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Invoke("DestroyTime", destroy);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);   
    }

    void DestroyTime()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string entityTag = collision.gameObject.tag;

        EntityStats health = collision.gameObject.GetComponent<EntityStats>();
        health.GiveDamage(weapon.getWeaponDamage());

    
        Destroy(gameObject);
    }

    public void setWeapon(TesstWeapons wp) {
        this.weapon = wp;
    }

}
