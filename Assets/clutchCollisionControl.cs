using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clutchCollisionControl : MonoBehaviour {

    public Collider myCollider;

	// Use this for initialization
	void Start () {
        Physics.IgnoreLayerCollision(9, 10);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        /**
        Debug.Log("hello");
        if(collision.gameObject.tag == "robot")
        {
            Physics.IgnoreCollision(collision.collider, myCollider);
        }
        */
        Physics.IgnoreCollision(collision.collider, myCollider);
    }
}
