using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimCamera : MonoBehaviour
{

    public GameObject target;
    public float rotateSpeed = 5;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position + (rotation * offset);

        transform.LookAt(target.transform);
    }

}
