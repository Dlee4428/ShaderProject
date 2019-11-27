using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private float time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifeDes());
    }

    private IEnumerator LifeDes()
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
