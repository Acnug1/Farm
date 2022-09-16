using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(0)]
[RequireComponent(typeof(NavMeshAgent))]

public class NavAgent : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        Enable(_agent);

        SetPositionOnNavMesh();
    }

    private void Enable(NavMeshAgent agent)
    {
        agent.enabled = true;
    }

    private void SetPositionOnNavMesh()
    {
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit closestHit, 4f, NavMesh.AllAreas))
        {
            transform.position = closestHit.position;
        }
    }

    public void Move(Vector3 direction, float speed)
    {
        Vector3 movement = direction * speed * Time.fixedDeltaTime;

        _agent.Move(movement);
    }
}
