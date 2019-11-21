using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public float m_rotSpeed = 1;
    public Transform m_target, m_player;
    private float m_mouseX, m_mouseY;

    public Transform obstruction;
    float zoomSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        CameraControl();
      //  ViewObstructed();
    }

    void CameraControl()
    {
        m_mouseX += Input.GetAxis("Mouse X") * m_rotSpeed;
        m_mouseY -= Input.GetAxis("Mouse Y") * m_rotSpeed;
        m_mouseY = Mathf.Clamp(m_mouseY, -35, 60);

        transform.LookAt(m_target);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_target.rotation = Quaternion.Euler(m_mouseY, m_mouseX, 0);
        }
        else
        {
            m_target.rotation = Quaternion.Euler(m_mouseY, m_mouseX, 0);
            m_player.rotation = Quaternion.Euler(0, m_mouseX, 0);
        }
    }

    //void ViewObstructed()
    //{
    //    RaycastHit hit;

    //    if(Physics.Raycast(transform.position, m_target.position - transform.position, out hit, 4.5f))
    //    {
    //        if(hit.collider.gameObject.tag != "Player")
    //        {
    //            obstruction = hit.transform;
    //            obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = 
    //                UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

    //            if(Vector3.Distance(obstruction.position, transform.position) >= 3f && 
    //                Vector3.Distance(transform.position, m_target.position) >= 1.5f)
    //            {
    //                transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
    //            }
    //        }
    //        else
    //        {
    //            obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode =
    //                UnityEngine.Rendering.ShadowCastingMode.On;

    //            if(Vector3.Distance(transform.position, m_target.position) < 4.5f)
    //            {
    //                transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
    //            }
    //        }
    //    }
    //}
}
