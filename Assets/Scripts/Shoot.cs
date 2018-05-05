using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    [System.Serializable]
    public struct Gun {
        public float shootTime;
        public float reloadTime;
        public int ammoCapacity;
        public int currentAmmo;
        public float travelSpeed;
        public GameObject bulletPrefab;
        public float travelTime;
        public float horizontalKickback;
        public float verticalKickback;

        public Gun(
            float shootTime,
            float reloadTime,
            int ammoCapacity,
            int currentAmmo,
            float travelSpeed,
            GameObject bulletPrefab,
            float travelTime,
            float horizontalKickback,
            float verticalKickback
        ) {
            this.shootTime = shootTime;
            this.reloadTime = reloadTime;
            this.ammoCapacity = ammoCapacity;
            this.currentAmmo = currentAmmo;
            this.travelSpeed = travelSpeed;
            this.bulletPrefab = bulletPrefab;
            this.travelTime = travelTime;
            this.horizontalKickback = horizontalKickback;
            this.verticalKickback = verticalKickback;
        }
    }

    public Gun gun = new Gun(
        0.02f, // shootTime
        4.1f, // reloadTime
        10, // ammoCapacity
        10, // currentAmmo
        18, // travelSpeed
        null, // bulletPrefab
        1.5f, // travelTime
        1, // horizontalKickback
        1 // verticalKickback
    );
    public AbstractController controller;
    public Movement movement;
    public float relativeLaunchOffset = 0.1f;

    float mLastShootTime = 0;
    float mLastReloadTime = 0;

	// Use this for initialization
	void Start () {
        if (controller == null) controller = GetComponent<AbstractController>();
        if (movement == null) movement = GetComponent<Movement>();
        mLastShootTime = -gun.shootTime;
        mLastReloadTime = -gun.reloadTime;
	}
	
	// Update is called once per frame
	void Update () {
        float currentTime = Time.realtimeSinceStartup;
        bool isShooting = mLastShootTime + gun.shootTime > currentTime;
        bool isReloading =  mLastReloadTime + gun.reloadTime > currentTime;
        bool isBusy = isShooting || isReloading;
        if (controller.ShouldShoot() && !isBusy) {
            if (gun.currentAmmo > 0) {
                FireGun(currentTime);
            } else {
                FireWhenOutOfAmmo();
            }
        }
        if (controller.ShouldReload() && !isBusy) {
            Reload(currentTime);
        }
	}

    void FireGun(float currentTime) {
        Debug.Log("firing");
        mLastShootTime = currentTime;
        gun.currentAmmo--;
        GameObject bullet = GameObject.Instantiate(gun.bulletPrefab);

		Quaternion localRotation = Quaternion.AngleAxis(controller.GetPointingDegrees(), Vector3.down);
		Vector3 relativeLaunchPosition = localRotation * new Vector3(relativeLaunchOffset, 0, 0);
        Vector3 relativeLaunchDirection = relativeLaunchPosition / relativeLaunchPosition.magnitude;
        bullet.transform.position = this.transform.position + relativeLaunchPosition;
        bullet.transform.rotation = Quaternion.AngleAxis(controller.GetPointingDegrees() - 90, Vector3.down);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        rigidbody.velocity =  relativeLaunchDirection * gun.travelSpeed;
        DeleteAfter deleteAfter = bullet.GetComponent<DeleteAfter>();
        deleteAfter.duration = gun.travelTime;
        Debug.Log(relativeLaunchOffset + ": " + relativeLaunchPosition + " : " + rigidbody.velocity);

        movement.AddKickback(
            -relativeLaunchDirection * gun.horizontalKickback +
            new Vector3(0, gun.verticalKickback, 0)
        );
    }

    void FireWhenOutOfAmmo() {
        Debug.Log("out of ammo");
    }

    void Reload(float currentTime) {
        Debug.Log("reloading");
        mLastReloadTime = currentTime;
        gun.currentAmmo = gun.ammoCapacity;
    }
}
