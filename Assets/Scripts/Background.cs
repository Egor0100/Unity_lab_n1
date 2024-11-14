using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance; 
    [SerializeField] private float speed;  
    private float _lookAhead;
    
    void Update()
    {
        transform.position = new Vector3(player.position.x + _lookAhead, transform.position.y, transform.position.z);
        _lookAhead = Mathf.Lerp(_lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * speed);
    }
}
