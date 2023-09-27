using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    private Rigidbody2D rb;
    public Weapon weapon;

    public int destroy = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        Vector2 directionOther = (direction * 1000).normalized;

        rb.velocity = directionOther * bulletSpeed;
        Debug.Log(mousePosition + " " + transform.position + " " + rb.velocity + " " + direction + " " + directionOther);

        // Вычисляем угол поворота пули
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Invoke("DestroyTime", destroy);
    }

    void DestroyTime()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EntityStats enemy = hitInfo.GetComponent<EntityStats>();
        if (enemy != null)
        {
            enemy.GiveDamage(weapon.getDamage());
        }
        Destroy(gameObject);
    }
}