using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Получаем ввод от игрока
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Нормализуем вектор направления
        direction = new Vector2(horizontalInput, verticalInput).normalized;

    }

    private void FixedUpdate()
    {
        // Применяем нормализованный вектор направления к скорости
        rb.velocity = direction * speed;
    }
}