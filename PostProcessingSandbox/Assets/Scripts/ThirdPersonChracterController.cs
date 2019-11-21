using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonChracterController : MonoBehaviour
{
    public float m_speed;

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(horizontal, 0f, vertical) * m_speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }
}
