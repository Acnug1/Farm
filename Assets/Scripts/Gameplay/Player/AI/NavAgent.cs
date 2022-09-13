using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(0)]
[RequireComponent(typeof(NavMeshAgent))]

public class NavAgent : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.enabled = true;

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit closestHit, 4f, NavMesh.AllAreas))
        {
            transform.position = closestHit.position;
        }
    }

    public void Move(Vector3 direction, float speed)
    {
        Vector3 movement = direction * speed * Time.deltaTime;

        _agent.Move(movement);
    }
}
