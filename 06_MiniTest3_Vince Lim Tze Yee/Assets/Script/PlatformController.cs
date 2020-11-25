using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            /* player
            collision.gameObject
            */
            /* moving cube
            gameObject
            */

            collision.gameObject.transform.parent = gameObject.transform;

            //transform.position = new Vector3(transform.position.x, transform.position.y, Player.transform.position.z);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.transform.parent = null; //To nullify the parentship of the object that was formly on the platform
    }
}
