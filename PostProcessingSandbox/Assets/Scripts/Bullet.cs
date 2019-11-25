using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private DataFloat speed;
    [SerializeField] private DataFloat lifeTime;
    [Header("Reference")]
    [SerializeField] public Data data;
    // Use this for initialization
    void Start()
    {
        speed = data.Float(speed);
        lifeTime = data.Float(lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        transform.Translate(0, 0, speed.Value * dt);

        lifeTime.Value -= dt;
        if (lifeTime.Value < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Debug.Log(other.tag);
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}