using System.Collections;
using UnityEngine;

public class RotateObject : MonoBehaviour {

	private bool _isRotating;
	private Vector2 touchOrigin;
	private int horizontal = 0;
	private int vertical = 0;

	void Update()
	{
		if(Input.touchCount > 0)
		{
			Touch myTouch = Input.touches[0];

			if (myTouch.phase == TouchPhase.Began) {
				touchOrigin = myTouch.position;
			}
			else if (Input.GetTouch (0).phase == TouchPhase.Moved) {

				Vector2 touchPresent = myTouch.position;

				float x = touchPresent.x - touchOrigin.x;
				float y = touchPresent.y - touchOrigin.y;

				if (Mathf.Abs (x) > Mathf.Abs (y)) {
					transform.Rotate (new Vector3 (0, 0, transform.rotation.z + 5), Space.World);
				} else {
					transform.Rotate (new Vector3 (transform.rotation.x + 5, 0, 0), Space.World);
				}
			}
		}
	}
}