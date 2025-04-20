using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, 20f, player.position.z);
        }
    }
}
