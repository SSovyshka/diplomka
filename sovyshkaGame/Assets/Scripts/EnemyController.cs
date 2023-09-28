using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();

        // ���������, ��� � ��� ���� NavMesh �� �����, � ���� ����� ������������ ���.
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent �� ������ �� ������� �����.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ������������� ������� ���������� ��� NavMeshAgent ��� ������� ������
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}