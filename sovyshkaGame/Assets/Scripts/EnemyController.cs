using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    public float chaseRadius = 5f; // Радиус, в пределах которого враг начнет преследовать игрока
    public float returnRadius = 5f; // Радиус, в пределах которого враг вернется к последней позиции игрока
    public float wanderRadius = 5f; // Радиус блуждания
    public float wanderTimer = 5f; // Время между сменой точки блуждания

    private Vector3 initialPosition; // Начальная позиция врага
    private float timer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        initialPosition = transform.position; // Сохраняем начальную позицию
        timer = wanderTimer;
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= chaseRadius)
        {
            // Игрок находится в радиусе, начинаем преследование
            initialPosition = target.position;
            agent.SetDestination(target.position);
        }
        else if (timer <= 0)
        {
            // Если время блуждания истекло, выбираем новую случайную точку в радиусе wanderRadius
            Vector3 randomPoint = Random.insideUnitSphere * wanderRadius;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPoint, out hit, wanderRadius, 1);
            agent.SetDestination(hit.position);
            timer = wanderTimer;
        }
        else
        {
            // Время блуждания ещё не истекло, уменьшаем таймер
            timer -= Time.deltaTime;
        }
    }
}