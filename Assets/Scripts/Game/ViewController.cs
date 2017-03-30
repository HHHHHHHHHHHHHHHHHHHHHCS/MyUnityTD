using UnityEngine;
using System.Collections;

public class ViewController : MonoBehaviour
{
    public float moveSpeed = 50;
    public float scrollSpeed = 20;


    private bool isDown = false;
    private Vector3 mouseDownPos;
    private const float maxSize = 35;
    private const float minSize = 10;
    private const float offsetSize = 25;
    private const float zMaxBorder = 18.5f;
    private const float xMaxBorder = 35.5f;

    private Camera mainCamera;

    public void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    public void Update()
    {
        Move();
        ZoomScale();
    }

    void CheckMoveBorder(Vector3 finalPos)
    {
        float xBorder = Mathf.Lerp(0, xMaxBorder, 1 - (mainCamera.orthographicSize - minSize) / offsetSize);
        float zBorder = Mathf.Lerp(0, zMaxBorder, 1 - (mainCamera.orthographicSize - minSize) / offsetSize);
        finalPos.x = Mathf.Clamp(finalPos.x, -xBorder, xBorder);
        finalPos.z = Mathf.Clamp(finalPos.z, -zBorder, zBorder);
        transform.position = finalPos;
    }


    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDown = true;
            mouseDownPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && isDown)
        {
            Vector3 newPos = Input.mousePosition;
            if (newPos != mouseDownPos)
            {

                Vector3 offset = mouseDownPos - newPos;
                mouseDownPos = newPos;
                offset.z = offset.y;
                offset.y = 0;
                Vector3 finalPos = transform.position + offset.normalized * moveSpeed * Time.deltaTime
                    * Mathf.Lerp(0.5f, 1, (mainCamera.orthographicSize - minSize) / offsetSize);
                CheckMoveBorder(finalPos);
            }
        }

    }


    void ZoomScale()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {

            float finalSize = mainCamera.orthographicSize
                + (Input.GetAxis("Mouse ScrollWheel") < 0 ? -1 : 1) * scrollSpeed * Time.deltaTime;

            mainCamera.orthographicSize = Mathf.Clamp(finalSize, minSize, maxSize);
            CheckMoveBorder(transform.position);
        }
    }
}
