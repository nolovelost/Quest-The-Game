using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartToLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToLoad()
    {
//just turn stuff on/off on the canvas to show lore

    }

    public void ToStreet()
    {
        SceneManager.LoadScene("Street");

    }
}
