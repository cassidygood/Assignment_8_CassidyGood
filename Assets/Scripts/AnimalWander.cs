using UnityEngine;
using UnityEngine.AI;

public class AnimalWander : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    private NavMeshAgent agent;
    private float timer;
    private bool isSleeping = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        if (isSleeping) return; // Skip movement when sleeping

        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public void SetSleeping(bool sleep)
    {
        isSleeping = sleep;
        agent.isStopped = sleep; // This will stop the animal's movement if it is sleeping
    }

    // Utility method to find a random point on the NavMesh
    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}

