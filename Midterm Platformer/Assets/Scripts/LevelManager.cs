using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    

    public string _levelName;

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("Player") && PlayerScript._hasKey){
            SceneManager.LoadScene(_levelName);
        }
    }
}
