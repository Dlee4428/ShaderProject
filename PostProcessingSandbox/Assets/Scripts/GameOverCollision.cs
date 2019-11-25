using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCollision : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]public MainMenuManager manager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(manager.GetComponent<MainMenuManager>().mainMenu);
        }
    }
}
