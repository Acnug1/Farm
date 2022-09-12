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
    }

    public void Move(Vector3 direction, float speed)
    {
        _agent.velocity = direction * speed * Time.fixedDeltaTime;
    }

    public void Stop()
    {
        _agent.velocity = Vector3.zero;
    }
}
