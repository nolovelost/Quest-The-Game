using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using PDollarGestureRecognizer;

public class PanelDraw : MonoBehaviour
{
    //test
   // public GameObject boom;
   // public GameObject whush;

    //The Panel that we will draw on
    public RectTransform UIPanel;
    public Canvas canvas;

    //Particles
    public ParticleSystem magic;
   

    public Transform gestureOnScreenPrefab;

    private List<Gesture> savedGestures = new List<Gesture>();

    private List<Point> points = new List<Point>();
    private int strokeId = -1;

    private Vector3 virtualKeyPosition = Vector2.zero;
    //This is the acutal Draw area
    private Rect drawArea;

    private RuntimePlatform platform;
    private int vertexCount = 0;

    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private LineRenderer currentGestureLineRenderer;

    //GUI
    private string message;
    private bool recognized;
    private string newGestureName = "";

    void Awake()
    {//not active on awake
        UIPanel.gameObject.SetActive(false);
       // boom.SetActive(false);
       // whush.SetActive(false);
    }
    void Start()
    {

        platform = Application.platform;
        //OLD
        //drawArea = new Rect(0, 0, Screen.width - Screen.width / 3, Screen.height);
        
        //NEW,making sure it scales
       /*
         drawArea = new Rect(UIPanel.rect.xMin, UIPanel.rect.yMax, UIPanel.rect.width  * canvas.scaleFactor, UIPanel.rect.height * canvas.scaleFactor);
        */
        //drawArea = new Rect(UIPanel.anchoredPosition.x, UIPanel.anchoredPosition.y, UIPanel.sizeDelta.x, UIPanel.sizeDelta.y);
        //Load pre-made spells
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/Spells/");
        foreach (TextAsset gestureXml in gesturesXml)
            savedGestures.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        //NOT USED HERE
        /*
        //Load user custom gestures
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
            */
    }
    
    void Update()
    {
        

        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            }
        }

        if (drawArea.Contains(virtualKeyPosition))
        {

            if (Input.GetMouseButtonDown(0))
            {

                if (recognized)
                {

                    recognized = false;
                    strokeId = -1;

                    points.Clear();

                    foreach (LineRenderer lineRenderer in gestureLinesRenderer)
                    {

                        lineRenderer.SetVertexCount(0);
                        Destroy(lineRenderer.gameObject);
                    }

                    gestureLinesRenderer.Clear();
                }

                ++strokeId;

                Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
                currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

                gestureLinesRenderer.Add(currentGestureLineRenderer);

                vertexCount = 0;
            }

            if (Input.GetMouseButton(0))
            {
                points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));
                //particle
                magic.transform.SetPositionAndRotation((Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10))),Quaternion.identity);

                currentGestureLineRenderer.SetVertexCount(++vertexCount);
                currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
            }
        }
    }
    
    void OnGUI()
    {
        GUI.backgroundColor = Color.red;
        GUI.Box(drawArea, "Draw Area");
    }
    
#region MyFunctions
    public void Recoginze()
    {
        recognized = true;

        Gesture beingTested = new Gesture(points.ToArray());
        Result result = PointCloudRecognizer.Classify(beingTested, savedGestures.ToArray());
         print("name: " + result.GestureClass + " score: " + result.Score);
      /*  if (result.Score >= 0.7f )
        {
           
        }
        else
        {
            print("WRONG");
        }*/
        }
        ///////////////TEST
        /*
        if (result.GestureClass == "glyph")
        {
            print("BOOM");
        GameObject  tempBoom =  Instantiate(boom,transform,true);
            Destroy(tempBoom, 1.25f);
          
        }
        else if (result.GestureClass == "NewLine")
        {
            GameObject tempWhush = Instantiate(whush, transform, true);
            Destroy(tempWhush, 1.0f);
           
           
        }
        */
       


        //  return result;
        /*
    }
    IEnumerator DisableW()
    {
       
        yield return new WaitForSeconds(1.0f);
        whush.SetActive(false);
    }
    IEnumerator DisableB()
    {

        yield return new WaitForSeconds(1.25f);
        boom.SetActive(false);
    }*/
    public void StartDrawing()
    {
        // activate ane set up the draw area
        UIPanel.gameObject.SetActive(true);
         drawArea = new Rect(UIPanel.rect.xMin, UIPanel.rect.yMax, UIPanel.rect.width  * canvas.scaleFactor, UIPanel.rect.height * canvas.scaleFactor);


    }

    public void StopDrawing()
    {
        //disable the draw area
        UIPanel.gameObject.SetActive(false);
        drawArea.size = new Vector2(0, 0);

        //clean up drawing
        recognized = false;
        strokeId = -1;

        points.Clear();

        foreach (LineRenderer lineRenderer in gestureLinesRenderer)
        {

            lineRenderer.SetVertexCount(0);
            Destroy(lineRenderer.gameObject);
        }

        gestureLinesRenderer.Clear();

        /////////TEST
    //    boom.SetActive(false);
    //    whush.SetActive(false);

    }


    #endregion





    /*
     * 
     * OLD
     * LEFT AS TEMPLATE
    void OnGUI()
    {

        GUI.Box(drawArea, "Draw Area");

        GUI.Label(new Rect(10, Screen.height - 40, 500, 50), message);

        if (GUI.Button(new Rect(Screen.width - 100, 10, 100, 30), "Recognize"))
        {

            recognized = true;

            Gesture candidate = new Gesture(points.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

            message = gestureResult.GestureClass + " " + gestureResult.Score;
        }

        GUI.Label(new Rect(Screen.width - 200, 150, 70, 30), "Add as: ");
        newGestureName = GUI.TextField(new Rect(Screen.width - 150, 150, 100, 30), newGestureName);

        if (GUI.Button(new Rect(Screen.width - 50, 150, 50, 30), "Add") && points.Count > 0 && newGestureName != "")
        {

            string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName, DateTime.Now.ToFileTime());

#if !UNITY_WEBPLAYER
            GestureIO.WriteGesture(points.ToArray(), newGestureName, fileName);
#endif

            trainingSet.Add(new Gesture(points.ToArray(), newGestureName));

            newGestureName = "";
        }
    }
    */
}
