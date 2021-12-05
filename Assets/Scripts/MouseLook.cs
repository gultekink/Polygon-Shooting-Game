using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensivity = 50;
    public float gunRotation;
    public Transform camera;

    private int currentGun =0;

    public float xRotation = 0f;
    public float yRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraLook();
    }

    void CameraLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
       
        yRotation += mouseX;
        yRotation = Mathf.Clamp(-90f, yRotation, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
      
        camera.Rotate(Vector3.up * mouseX*gunRotation);
       
        

    }

}
