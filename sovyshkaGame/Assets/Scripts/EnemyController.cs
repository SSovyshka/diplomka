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

        // Убедитесь, что у вас есть NavMesh на сцене, и враг может использовать его.
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent не найден на объекте врага.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Устанавливаем позицию назначения для NavMeshAgent как позицию игрока
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}