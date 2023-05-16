using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject placementPlane;
    public Material placementPlaneNormal;
    public Material placementPlaneCollision;

    private GameObject objectToPlace = null;
    private GameObject toBeMoved = null;

    public Button tableButton;
    public Button chairButton;
    public Button sofaButton;
    public Button deleteButton;
    public Button replaceButton;

    private Pose PlacementPose;
    private bool placementPoseIsValid = false;
    private bool collisionDetected = false;

    private ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Camera arCam;

    // Start is called before the first frame update
    void Start()
    {
        
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();

        Button button1 = tableButton.GetComponent<Button>();
		button1.onClick.AddListener(SelectTable);

        Button button2 = chairButton.GetComponent<Button>();
		button2.onClick.AddListener(SelectChair);

        Button button3 = sofaButton.GetComponent<Button>();
		button3.onClick.AddListener(SelectSofa);

        Button button4 = deleteButton.GetComponent<Button>();
		button4.onClick.AddListener(DeleteObject);

        Button button5 = replaceButton.GetComponent<Button>();
		button5.onClick.AddListener(ReplaceObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (Input.touchCount == 0) 
            return;

        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);
    
        if (aRRaycastManager.Raycast(Input.GetTouch(0).position, hits)) 
        {
            Debug.Log("Touch Began");
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit)) 
                {
                    Debug.Log("Physics Raycast");
                    if (hit.collider.gameObject.tag == "Spawnable")
                    {
                        Debug.Log("Object hit: " + hit.collider.gameObject);
                        toBeMoved = hit.collider.gameObject;
                    }
                }
            }
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(screenCenter);

        if (aRRaycastManager.Raycast(screenCenter, hits)) 
        {
            if (Physics.Raycast(ray, out hit)) 
            {
                Debug.Log("Physics Raycast");
                if (hit.collider.gameObject.tag == "Spawnable")
                {
                    Debug.Log("Collision detected");
                    collisionDetected = true;
                }
                else
                {
                    collisionDetected = false;
                }
            }
        }

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid) 
        {
            PlacementPose = hits[0].pose;
        }
    }

    private void UpdatePlacementIndicator() 
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
            var placementPlaneRenderer = placementPlane.GetComponent<Renderer>();

            if (collisionDetected)
            {
                // Change color
                placementPlaneRenderer.material = placementPlaneCollision;
            } else 
            {
                // Change to another color
                placementPlaneRenderer.material = placementPlaneNormal;
            }
        }
        else 
        {
            placementIndicator.SetActive(false);
        }
    }

    private void PlaceObject() 
    {
        Instantiate(objectToPlace, PlacementPose.position, PlacementPose.rotation);
    }

    private void SelectTable()
    {
        if (collisionDetected)
        {
            return;
        }
        objectToPlace = (GameObject) Resources.Load("Prefabs/Table", typeof(GameObject));
        if (placementPoseIsValid && Input.touchCount > 0)
        {
            PlaceObject();
        }
    }

    private void SelectChair()
    {
        if (collisionDetected)
        {
            return;
        }
        objectToPlace = (GameObject) Resources.Load("Prefabs/Chair", typeof(GameObject));
        if (placementPoseIsValid && Input.touchCount > 0)
        {
            PlaceObject();
        }
    }

    private void SelectSofa()
    {
        if (collisionDetected)
        {
            return;
        }
        Debug.Log("Sofa selected");
        objectToPlace = (GameObject) Resources.Load("Prefabs/Sofa", typeof(GameObject));
        if (placementPoseIsValid && Input.touchCount > 0)
        {
            PlaceObject();
        }
    }

    private void DeleteObject()
    {
        Debug.Log("Object to be deleted: " + toBeMoved);
        if (toBeMoved)
        {
            // toBeMoved.SetActive(false);
            Destroy(toBeMoved);
            toBeMoved = null;
        }
    }

    private void ReplaceObject()
    {
        if (collisionDetected)
        {
            return;
        }
        if (toBeMoved)
        {
            Instantiate(toBeMoved, PlacementPose.position, PlacementPose.rotation);
            Destroy(toBeMoved);
            toBeMoved = null;
        }
    }
}
