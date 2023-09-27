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

        // �������� ���� �� ������
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // ����������� ������ �����������
        direction = new Vector2(horizontalInput, verticalInput).normalized;

    }

    private void FixedUpdate()
    {
        // ��������� ��������������� ������ ����������� � ��������
        rb.velocity = direction * speed;
    }
}