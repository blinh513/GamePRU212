using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePoints : MonoBehaviour
{
    [SerializeField]
    private float distance=2;
    private GameObject pointPrefab;
    private Vector3 pointAPosition;
    private Vector3 pointBPosition;
    private GameObject pointA;
    private GameObject pointB;
    // Start is called before the first frame update
    void Start()
    {
        pointPrefab= GetComponent<GameObject>();
        // Lấy vị trí của game object empty
        Vector3 emptyPosition = transform.position;

        // Tạo điểm A bên trái
        pointAPosition = emptyPosition + Vector3.left * distance; // Điểm A nằm bên trái, cách empty 2 đơn vị
        pointA = Instantiate(pointPrefab, pointAPosition, Quaternion.identity);

        // Tạo điểm B bên phải
        pointBPosition = emptyPosition + Vector3.right * distance; // Điểm B nằm bên phải, cách empty 2 đơn vị
        pointB = Instantiate(pointPrefab, pointBPosition, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
