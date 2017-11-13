using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FallToHallway : MonoBehaviour {

	
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("switch");
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("Underground_2");
        }
    }
}
