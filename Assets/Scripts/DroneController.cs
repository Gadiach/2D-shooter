using UnityEngine;

public class DroneController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform bombDropPoint;
    [SerializeField] private float speed = 5f;

    void Update()
    {
        // ”правление дроном (вверх, вниз, влево, вправо)
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(new Vector3(moveX, moveY, 0));

        // —брос бомбы при нажатии пробела
        if (Input.GetKeyDown(KeyCode.C))
        {
            DropBomb();
        }
    }

    void DropBomb()
    {
        Instantiate(bombPrefab, bombDropPoint.position, Quaternion.identity);
    }
}