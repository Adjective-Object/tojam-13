using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : AbstractController {
	public string xAxisMovement = "Horizontal";
	public string yAxisMovement = "Vertical";

	public string xAxisPointing = "Horizontal";
	public string yAxisPointing = "Vertical";

    public string jumpButton = "Jump";
    public string shootButton = "Fire";
    public string reloadButton = "Reload";
	public float pointingAxisThreshold = 0.01f;
	public Camera mousePointingRaycastOrigin;
	public bool useMousePointing = true;

	void Start() {
		if (mousePointingRaycastOrigin == null) mousePointingRaycastOrigin = Camera.main;
	}

	public override Vector2 GetIntendedVelocity() {
 		return new Vector2(Input.GetAxis(xAxisMovement), Input.GetAxis(yAxisMovement));
	}

	protected override bool ShouldSetPointing() {
		if (useMousePointing) {
			RaycastHit hit;
			Ray ray = mousePointingRaycastOrigin.ScreenPointToRay(Input.mousePosition);
			bool hitSomething = Physics.Raycast(ray, out hit);
			return hitSomething;
		} else {
			return Mathf.Abs(Input.GetAxis(xAxisPointing)) > pointingAxisThreshold ||
				Mathf.Abs(Input.GetAxis(yAxisPointing)) > pointingAxisThreshold;
		}
	}

	protected override float GetIntendedPointingDegrees() {
		if (useMousePointing) {
			return GetIntendedPointingDegreesFromMouse();
		}
		return (Mathf.Atan2(Input.GetAxis(yAxisPointing), Input.GetAxis(xAxisPointing))) * Mathf.Rad2Deg;
	}

	private float GetIntendedPointingDegreesFromMouse() {
		// Raycast into scene from camera
		RaycastHit hit;
        Ray ray = mousePointingRaycastOrigin.ScreenPointToRay(Input.mousePosition);
		if (!Physics.Raycast(ray, out hit)) {
			Debug.LogWarning("failed to raycast");
			return 0f;
		}

		Vector3 objectHitPosition = hit.point;
		Vector3 positionDifference = objectHitPosition - this.transform.position;
		return Mathf.Atan2(positionDifference.z, positionDifference.x) * Mathf.Rad2Deg; 
	}

    public override bool ShouldJump() {
        return Input.GetButtonDown(jumpButton);
    }

    public override bool ShouldShoot() {
        return Input.GetButton(shootButton);
    }

    public override bool ShouldReload() {
        return Input.GetButtonDown(reloadButton);
    }

	public override void Die() {
		Debug.Log("Death of Player");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}