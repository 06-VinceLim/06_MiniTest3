using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject PlayerGO; // Public game object for you to set which object you want to tie to. Check your unity
    public Vector3 posOffset;
   // Vector3 posOffset = new Vector3(0, 4.0f, -4.0f); //(X,Y,Z) Position offset so your camera don't directly go to your game object and keep a distance based on the X Y Z offset
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerGO.transform.position + posOffset, 0.1f); //Having the camera focus and follow the gameobject and maintain a certain distance base on off set, 0.1f is to ensure it don't follow your cube super fast
    }
}


