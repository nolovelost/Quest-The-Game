using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnClick : MonoBehaviour {

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
	
}
