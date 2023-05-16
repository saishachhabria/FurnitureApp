using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public Button rotateButton;

    // Start is called before the first frame update
    void Start()
    {
        // objectToPlace = GameObject.Find("T");
        Button btn = rotateButton.GetComponent<Button>();
		btn.onClick.AddListener(Rotate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate()
    {
		Debug.Log ("You have clicked the rotate button!");
        objectToPlace.transform.Rotate(90, 0, 0);
	}
}
