using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortParticle : MonoBehaviour {
    private const int IsometricRangePerYUnit = 100;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Renderer renderPart = this.transform.GetComponent<ParticleSystemRenderer>();
        renderPart.sortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit);
    }
}
