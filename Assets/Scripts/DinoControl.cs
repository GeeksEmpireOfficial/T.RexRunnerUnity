using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DinoControl : MonoBehaviour {

	Animator anim;
	Rigidbody2D rigidbody;

	float upOrDown;

    [SerializeField]
    float jumpForce = 500f;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.gameStopped != true) {
			upOrDown = CrossPlatformInputManager.GetAxisRaw ("Vertical");
			if (upOrDown > 0 && rigidbody.velocity.y == 0) {
                rigidbody.AddForce(Vector2.up * jumpForce); 
            }

            /*if (upOrDown < 0 && rigidbody.velocity.y == 0)
            {
                anim.SetBool("isDown", true);
            }
            else
            {
                anim.SetBool("isDown", false);
            }*/
        }
    }
}
