using UnityEngine;
public class PlayerController : AbstractController {
	public string xAxisMovement = "Horizontal";
	public string yAxisMovement = "Vertical";

	public string xAxisPointing = "Horizontal";
	public string yAxisPointing = "Vertical";

    public string jumpButton = "Jump";
    public string shootButton = "Fire";
    public string reloadButton = "Reload";
	public float pointingAxisThreshold = 0.01f;

	public override Vector2 GetIntendedVelocity() {
 		return new Vector2(Input.GetAxis(xAxisMovement), Input.GetAxis(yAxisMovement));
	}

	protected override bool ShouldSetPointing() {
		return Mathf.Abs(Input.GetAxis(xAxisPointing)) > pointingAxisThreshold ||
			Mathf.Abs(Input.GetAxis(yAxisPointing)) > pointingAxisThreshold;
	}

	protected override float GetIntendedPointingDegrees() {
 		return (Mathf.Atan2(Input.GetAxis(yAxisPointing), Input.GetAxis(xAxisPointing))) * Mathf.Rad2Deg;
	}

    public override bool ShouldJump() {
        return Input.GetButtonDown(jumpButton);
    }

    public override bool ShouldShoot() {
        return Input.GetButtonDown(shootButton);
    }

    public override bool ShouldReload() {
        return Input.GetButtonDown(reloadButton);
    }

	public override void Die() {
		Debug.Log("Death of Player");
	}

}