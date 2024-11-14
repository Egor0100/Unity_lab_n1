using UnityEngine;

public class BackgroundVertical : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.position.y + 3f, transform.position.z);
    }
}
