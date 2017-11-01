using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sort : MonoBehaviour {
    private const int IsometricRangePerYUnit = 100;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        Renderer renderer = this.transform.GetComponent<SpriteRenderer>();
        renderer.sortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit);
    }
}
