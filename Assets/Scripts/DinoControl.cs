using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DinoControl : MonoBehaviour {

	Rigidbody2D rigidbody;

    [SerializeField]
    float jumpForce = 500f;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.gameStopped != true) {		
            if (Input.GetMouseButtonDown(0))
            {
                rigidbody.AddForce(Vector2.up * jumpForce);
            }
        }
    }
}
