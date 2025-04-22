using UnityEngine;
using UnityEngine.AI;

public class AnimalWander : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public Transform player;
    public float barkDistance = 5f;
    public string[] barkLines = { "Bark!", "Woof!", "Grr!" };

    private NavMeshAgent agent;
    private float timer;
    private bool isSleeping = false;
    private bool hasBarked = false;

    private AnimalBarkUI barkUI; // ✅ Declare UI reference

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;

        // ✅ Cache the bark UI object once for performance
        barkUI = Object.FindFirstObjectByType<AnimalBarkUI>();
    }

    void Update()
    {
        if (isSleeping) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < barkDistance && !hasBarked)
        {
            if (barkUI != null && barkLines.Length > 0)
            {
                int index = Random.Range(0, barkLines.Length);
                barkUI.ShowBark(barkLines[index]);
            }
            hasBarked = true;
        }
        else if (distanceToPlayer >= barkDistance)
        {
            hasBarked = false;
        }

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
