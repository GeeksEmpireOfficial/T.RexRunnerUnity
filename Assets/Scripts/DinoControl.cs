using UnityEngine;

public class DinoControl : MonoBehaviour {

	Rigidbody2D rigidbody;

    [SerializeField]
    float jumpForce = 500f;


    bool isJumping = false;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.gameStopped != true) {

            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                if (Input.GetMouseButtonDown(0)
                || Input.GetKeyDown(KeyCode.UpArrow)
                || Input.GetKeyDown(KeyCode.Space)
                || Input.GetKeyDown(KeyCode.J))
                {
                    if (!isJumping)
                    {
                        isJumping = true;
                        rigidbody.AddForce(Vector2.up * jumpForce);
                    }
                }
            } else
            {
                if ((Input.GetTouch(0).position.y >= (Screen.height / 2))
                        || Input.GetKeyDown(KeyCode.Space)
                        || Input.GetKeyDown(KeyCode.J))
                {
                    if (!isJumping)
                    {
                        isJumping = true;
                        rigidbody.AddForce(Vector2.up * jumpForce);
                    }
                }

                if ((Input.GetTouch(0).position.y < (Screen.height / 2)))
                {
                    //rigidbody.AddForce(Vector2.up * jumpForce);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.name.Equals("GroundBW") || collider2D.gameObject.name.Equals("GroundBW_Second"))
        {
            isJumping = false;
        }
    }
}
