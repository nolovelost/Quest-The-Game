using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalAttack : MonoBehaviour {
    public bool ready = false;
    public float fireRate = 0.5f;
    public GameObject target;
    public GameObject whush;
    public AudioSource conjure;

    
    IEnumerator Go()
    {
        Fire();
        ready = false;
        yield return new WaitForSeconds(fireRate);
        ready = true;
    }

    void Fire()
    {
        whush.GetComponent<EnemyBolt>().lockedTarget = target;
        conjure.Play();
        Instantiate(whush, this.transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
    StartCoroutine("Go");
        }
        
    }

}
