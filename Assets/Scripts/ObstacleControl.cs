using UnityEngine;

public class ObstacleControl : MonoBehaviour {

	[SerializeField]
	float moveSpeed = -5f;

	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (transform.position.x + moveSpeed * Time.deltaTime,
			transform.position.y);
		
		if (transform.position.x < -13f)
			Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider2D)
	{
		if (collider2D.gameObject.name.Equals("TRex"))
        {
            GameControl.instance.DinoHit();
        }
			
	}

}
