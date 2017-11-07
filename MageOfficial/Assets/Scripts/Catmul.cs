using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catmul : MonoBehaviour {

  //  public GameObject[] path;
    //for visualization
    public List<Vector3> newPoints = new List<Vector3>();
    //how many points on the curve, smoothness ???
    public float amountOfPoints = 10.0f;
    //what is this ?? apparently 0-1
    public float alpha = 0.5f;

    //public Vector3[] curr;
  //  public Curver newCurve;
	// Use this for initialization
	void Start () {
   //     newCurve = this.transform.GetComponent<Curver>();
	}
	
	// Update is called once per frame
	void Update () {
       // CatmulRomCalculate();
   //     getCurve();
	}


  /* void getCurve()
    {
        Vector3 p0 = new Vector3(path[0].transform.position.x, path[0].transform.position.y,0);
        Vector3 p1 = new Vector3(path[1].transform.position.x, path[1].transform.position.y,0);
        Vector3 p2 = new Vector3(path[2].transform.position.x, path[2].transform.position.y,0);
        Vector3 p3 = new Vector3(path[3].transform.position.x, path[3].transform.position.y,0);

        Vector3[] pathC = { p0, p1, p2, p3 };
      //  curr = newCurve.MakeSmoothCurve(pathC, 3);
      //  return newCurve.MakeSmoothCurve(path, 3);
    }*/
   public  Vector3[] CatmulRomCalculate(Vector2 start, Vector2 end)
    {
        //fresh list
        newPoints.Clear();

        //need 4 points for the curve
        Vector2 p0 = new Vector2(0, 0);
        Vector2 p1 = start;
        Vector2 p2 = end;
        Vector2 p3 = new Vector2(0, 0);
        //knot sequences
        float t0 = 0.0f;
        float t1 = GetT(t0, p0, p1);
        float t2 = GetT(t1, p1, p2);
        float t3 = GetT(t2, p2, p3);

        //calculation
        for (float i = t1; i < t2; i+=((t2-t1)/amountOfPoints))
        {
            //first step
            Vector2 A1 = (t1 - i) / (t1 - t0) * p0 + (i - t0) / (t1 - t0)  * p1;
            Vector2 A2 = (t2 - i) / (t2 - t1) * p1 + (i - t1) / (t2 - t1)  * p2;
            Vector2 A3 = (t3 - i) / (t3 - t2) * p2 + (i - t2) / (t3 - t2)  * p3;
            //second step
            Vector2 B1 = (t2 - i) / (t2 - t0) * A1 + (i - t0) / (t2 - t0) * A2;
            Vector2 B2 = (t3 - i) / (t3 - t1) * A2 + (i - t1) / (t3 - t1) * A3;
            //last step
            Vector2 C = (t2 - i) / (t2 - t1) * B1 + (i - t1) / (t2 - t1) * B2;
            //add the final point

            newPoints.Add(new Vector3(C.x,C.y,0));
        }
        return newPoints.ToArray();
    }

    float GetT(float t, Vector2 start, Vector2 end)
    {
        float a = Mathf.Pow((end.x - start.x), 2.0f) + Mathf.Pow((end.y - start.y), 2.0f);
        float b = Mathf.Pow(a, 0.5f);
        float c = Mathf.Pow(b, alpha);

        return (c + t);
    }

    //visualization for testing
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector2 x in newPoints)
        {
            Vector3 position = new Vector3(x.x, x.y, 0);
            Gizmos.DrawSphere(position, 0.3f);
        }/*
        Gizmos.color = Color.blue;
        foreach (Vector3 x in curr)
        {
            Gizmos.DrawSphere(x, 0.3f);
        }
    }*/
    }
}
