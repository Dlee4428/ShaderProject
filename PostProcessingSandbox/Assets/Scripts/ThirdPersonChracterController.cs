using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonChracterController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private DataFloat m_bulletTimer;
    [SerializeField] private DataFloat m_bulletCoolDown;
    [SerializeField] private DataFloat m_speed;
    [Header("Reference")]
    [SerializeField] public GameObject bullet;
    [SerializeField] public Transform gun;
    [SerializeField] public Data data;
    private void Start()
    {
        m_bulletTimer = data.Float(m_bulletTimer);
        m_bulletCoolDown = data.Float(m_bulletCoolDown);
        m_speed = data.Float(m_speed);
    }


    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        m_bulletTimer.Value -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_bulletTimer.Value < 0)
            {
                Debug.Log("Fire");
                m_bulletTimer.Value = m_bulletCoolDown.Value;
                Instantiate(bullet, gun.position, gun.transform.rotation);
            }
        }
    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(horizontal, 0f, vertical) * m_speed.Value * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }
}
