using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    private Rigidbody2D rb;
    public Weapon weapon;


    //   void Start()
    //   {
    //       rb = GetComponent<Rigidbody2D>();
    //   Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //   Vector2 direction = (mousePosition - transform.position).normalized;
    //
    //   float weaponAccuracy = weapon.accuracy;
    //
    //   float spread = Random.Range(-weaponAccuracy, weaponAccuracy);
    //   Quaternion spreadRotation = Quaternion.Euler(0f, 0f, spread);
    //   direction = spreadRotation * direction;
    //
    //   Vector2 directionOther = (direction * 1000).normalized;
    //
    //   rb.velocity = directionOther * bulletSpeed;
    //   Debug.Log(mousePosition + " " + transform.position + " " + rb.velocity + " " + direction + " " + directionOther);
    //
    //   // Вычисляем угол поворота пули
    //   float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //   transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //
    //  Invoke("DestroyTime", destroy);
    //}

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float weaponAccuracy = weapon.accuracy;
        float spread = Random.Range(-weaponAccuracy, weaponAccuracy);
        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, spread);
        Vector2 direction = spreadRotation * transform.right; // Используем transform.right как начальное направление пули

        Vector2 directionOther = (direction * 1000).normalized;

        rb.velocity = directionOther * bulletSpeed;

        // Вычисляем угол поворота пули
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Invoke("DestroyTime", 3);
    }

    void DestroyTime()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.gameObject.CompareTag("Player"))
        {
            EntityStats enemy = hitInfo.GetComponent<EntityStats>();
            if (enemy != null)
            {
                enemy.GiveDamage(weapon.getDamage());
            }
            Destroy(gameObject);
        }
    }
}