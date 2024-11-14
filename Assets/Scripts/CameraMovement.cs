using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance; // Дистанция, на которую камера будет "смотреть вперед" относительно игрока
    [SerializeField] private float speed;  // Скорость, с которой камера будет перемещаться вперед
    private float _lookAhead;
    
    void Update()
    {
        // Обновление позиции камеры с учетом перемещения игрока
        transform.position = new Vector3(player.position.x + _lookAhead, transform.position.y, transform.position.z);
        _lookAhead = Mathf.Lerp(_lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * speed);
    }
}
