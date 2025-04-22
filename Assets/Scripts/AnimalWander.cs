using UnityEngine;
using UnityEngine.AI;

public class AnimalWander : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public Transform player;
    public float barkDistance = 5f;

    private NavMeshAgent agent;
    private float timer;
    private bool isSleeping = false;
    private bool hasBarked = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        if (isSleeping) return; // Skip movement when sleeping

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Bark once when player is close
        if (distanceToPlayer < barkDistance && !hasBarked)
        {
            Object.FindFirstObjectByType<AnimalBarkUI>().ShowBark("Bark!");
            hasBarked = true;
        }
        else if (distanceToPlayer >= barkDistance)
        {
            hasBarked = false; // reset barking ability when player moves away
        }

        // Wandering logic
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
        agent.isStopped = sleep;
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
