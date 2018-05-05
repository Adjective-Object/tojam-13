using UnityEngine;
public class PlayerController : AbstractController {
	public string xAxisMovement = "Horizontal";
	public string yAxisMovement = "Vertical";

	public string xAxisPointing = "Horizontal";
	public string yAxisPointing = "Vertical";
	public float pointingAxisThreshold = 0.01f;

	public override Vector2 GetIntendedVelocity() {
 		return new Vector2(Input.GetAxis(xAxisMovement), Input.GetAxis(yAxisMovement));
	}

	public override bool ShouldSetPointing() {
		return Mathf.Abs(Input.GetAxis(xAxisPointing)) > pointingAxisThreshold ||
			Mathf.Abs(Input.GetAxis(yAxisPointing)) > pointingAxisThreshold;
	}

	public override float GetIntendedPointingDegrees() {
 		return (Mathf.Atan2(Input.GetAxis(yAxisPointing), Input.GetAxis(xAxisPointing))) * Mathf.Rad2Deg;
	}
}