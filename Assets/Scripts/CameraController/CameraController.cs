using UnityEngine;

public class CameraController : MonoBehaviour {

	public float maxX, minX; 
	public float minY,maxY;

	private Transform player;
	private const float EXTENT = 8f;

	// Update is called once per frame
	private void FixedUpdate () {
        if (PlayerMoving.myBody != null)
        {
            player = PlayerMoving.myBody.gameObject.transform;
            if (player != null && !PlayerHealth.control.GetDie())
            {
                float h = player.transform.localScale.x;
                FllowingCamera(h);
            }
        }
	}

	private void FllowingCamera(float h){
		if (player != null) {
			Vector3 Position = player.position;
			Position.z = this.transform.position.z;

			if (h > 0) {
				Position.x += EXTENT;
			} else if (h < 0) {
				Position.x -=  EXTENT;
			}

			// Min x camera
			if (Position.x < minX) {
				Position.x = minX;
			}
			// Max x camera
			if (Position.x > maxX) {
				Position.x = maxX;
			}

			// Min y camera
			if (Position.y < minY) {
				Position.y = minY;
			}

			// Max x camera
			if (Position.y > maxY) {
				Position.y = maxY;
			}

            transform.position = Vector3.Lerp(transform.position, Position, 2f * Time.deltaTime);
		}
	}
}
