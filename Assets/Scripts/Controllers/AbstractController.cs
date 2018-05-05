using UnityEngine;

public abstract class AbstractController : MonoBehaviour{
	private float mLastPointing = 0;

	public abstract Vector2 GetIntendedVelocity();
    public abstract bool ShouldJump();
    public abstract bool ShouldShoot();
    public abstract bool ShouldReload();
	protected abstract bool ShouldSetPointing();
	protected abstract float GetIntendedPointingDegrees();

	public float GetPointingDegrees() {
		if (ShouldSetPointing()) {
			mLastPointing = GetIntendedPointingDegrees();
		}
		return mLastPointing;
	}

	public void Die() {
		Object.Destroy(this.gameObject);
	}
}