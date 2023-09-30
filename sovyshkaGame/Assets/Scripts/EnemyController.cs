using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    public float chaseRadius = 5f; // ������, � �������� �������� ���� ������ ������������ ������
    public float returnRadius = 5f; // ������, � �������� �������� ���� �������� � ��������� ������� ������
    public float wanderRadius = 5f; // ������ ���������
    public float wanderTimer = 5f; // ����� ����� ������ ����� ���������

    private Vector3 initialPosition; // ��������� ������� �����
    private float timer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        initialPosition = transform.position; // ��������� ��������� �������
        timer = wanderTimer;
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= chaseRadius)
        {
            // ����� ��������� � �������, �������� �������������
            initialPosition = target.position;
            agent.SetDestination(target.position);
        }
        else if (timer <= 0)
        {
            // ���� ����� ��������� �������, �������� ����� ��������� ����� � ������� wanderRadius
            Vector3 randomPoint = Random.insideUnitSphere * wanderRadius;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPoint, out hit, wanderRadius, 1);
            agent.SetDestination(hit.position);
            timer = wanderTimer;
        }
        else
        {
            // ����� ��������� ��� �� �������, ��������� ������
            timer -= Time.deltaTime;
        }
    }
}