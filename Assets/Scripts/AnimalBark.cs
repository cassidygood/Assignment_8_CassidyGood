using UnityEngine;
using TMPro;

public class AnimalBark : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public TextMeshProUGUI barkText; // Assign this in the Inspector or use 3D Text

    private float barkCooldown = 3f;
    private float barkTimer = 0f;

    void Update()
    {
        barkTimer -= Time.deltaTime;

        Vector3 directionToPlayer = player.position - transform.position;

        if (directionToPlayer.magnitude <= detectionRange)
        {
            Ray ray = new Ray(transform.position, directionToPlayer.normalized);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player") && barkTimer <= 0f)
                {
                    Bark();
                    barkTimer = barkCooldown;
                }
            }
        }

        if (barkTimer <= 0f && barkText != null)
        {
            barkText.text = "";
        }
    }

    void Bark()
    {
        if (barkText != null)
        {
            barkText.text = "Bark!";
        }
    }
}
