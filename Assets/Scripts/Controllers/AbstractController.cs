using UnityEngine;

public abstract class AbstractController : MonoBehaviour{
	public abstract Vector2 GetIntendedVelocity();
    public abstract bool ShouldSetPointing();
	public abstract float GetIntendedPointingDegrees();
}