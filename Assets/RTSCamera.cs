using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float panBorderThickness = 10f;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 delta = Vector3.zero;

        if (horizontal > 0 || Input.mousePosition.x >= Screen.width - panBorderThickness)
            delta.x += moveSpeed * Time.deltaTime;

        if (horizontal < 0 || Input.mousePosition.x <= panBorderThickness)
            delta.x -= moveSpeed * Time.deltaTime;

        if (vertical > 0 || Input.mousePosition.y >= Screen.height - panBorderThickness)
            delta.z += moveSpeed * Time.deltaTime;

        if (vertical < 0 || Input.mousePosition.y <= panBorderThickness)
            delta.z -= moveSpeed * Time.deltaTime;

        transform.position += delta;
    }
}
