using UnityEngine;

public class DinoControl : MonoBehaviour {

	Rigidbody2D rigidbody;

    [SerializeField]
    float jumpForce = 500f;

    [SerializeField]
    Sprite TrexBend;

    bool isJumping = false;

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
                {//JumpUp
                    if (!isJumping)
                    {
                        /*if (Input.mousePosition.y >= (Screen.height / 3))
                        {
                            
                        } else
                        {

                        }*/
                        isJumping = true;
                        rigidbody.AddForce(Vector2.up * jumpForce);
                    }
                } else if(Input.GetKeyDown(KeyCode.DownArrow))//BendOver
                {
                    
                }
            } else
            {
                if (/*(Input.GetTouch(0).position.y >= (Screen.height / 3))*/
                        Input.GetKeyDown(KeyCode.Space)
                        || Input.GetKeyDown(KeyCode.J))
                {//JumpUp
                    if (!isJumping)
                    {
                        isJumping = true;
                        rigidbody.AddForce(Vector2.up * jumpForce);
                    }
                } else
                {//BendOver

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
