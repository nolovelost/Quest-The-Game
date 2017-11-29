using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PickUp : MonoBehaviour {

    public Image page;
    public float radius = 1.0f;
    public GameObject player;
    public GameObject Enemies;
    public ParticleSystem isInteractible;
    public AudioSource appearEnemy;
    public AudioSource appearPage;
    float isCloseEnough;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Raycast");
            CastRay();
        }
       
    }
    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        //must be close enough
        float isCloseEnough = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(this.transform.position.x, this.transform.position.y));
        Debug.Log("Distance" + isCloseEnough);
        //  Collider2D overlapped =  Physics2D.OverlapCircle(new Vector2(this.transform.position.x, this.transform.position.y), radius);
        //    Debug.Log(overlapped);
        if (hit)
        {
if (hit.collider.tag == "pick" && isCloseEnough <= 0.8f)
        {
            page.gameObject.SetActive(true);
                appearPage.Play();
                Debug.Log(hit.collider.gameObject.name);
            this.gameObject.SetActive(false);
            //so that it can be used for the other one as well
            if (Enemies != null)
            {
                Enemies.gameObject.SetActive(true);
                    appearEnemy.Play();
            }
        }
        


        }
    }
  
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("PARTICLES");
        isInteractible.Play();


    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("PARTICLES END");
        isInteractible.Stop();


    }
}