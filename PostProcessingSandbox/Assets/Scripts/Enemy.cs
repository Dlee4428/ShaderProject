using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Reference")]
    public NavMeshAgent navMeshAgent;
    public GameObject target;

    // Use this for initialization
    IEnumerator Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();


        while (true)
        {
            if (target != null)
            {
                navMeshAgent.SetDestination(target.transform.position);
            }
            yield return null;
        }
    }
}
