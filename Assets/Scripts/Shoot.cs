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
    }

    public Gun gun;
    public AbstractController controller;
    public float relativeLaunchOffset = 0.1f;

    float mLastShootTime = 0;
    float mLastReloadTime = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        mLastShootTime = -gun.shootTime;
        mLastReloadTime = -gun.reloadTime;
        float currentTime = Time.realtimeSinceStartup;
        bool isShooting = currentTime - mLastShootTime < gun.shootTime;
        bool isReloading = currentTime - mLastReloadTime < gun.reloadTime;
        bool isBusy = isShooting || isReloading;
        if (controller.ShouldShoot() && !isBusy) {
            if (gun.currentAmmo > 0) {
                FireGun(currentTime);
            } else {
                FireWhenOutOfAmmo();
            }
        }
        if (controller.ShouldReload() && !isBusy) {
            Debug.Log("Should Reload");
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
        bullet.transform.position = this.transform.position + relativeLaunchPosition;
        bullet.transform.rotation = Quaternion.AngleAxis(controller.GetPointingDegrees() - 90, Vector3.down);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = relativeLaunchPosition / relativeLaunchPosition.magnitude * gun.travelSpeed;
        DeleteAfter deleteAfter = bullet.GetComponent<DeleteAfter>();
        deleteAfter.duration = gun.travelTime;
        Debug.Log(relativeLaunchOffset + ": " + relativeLaunchPosition + " : " + rigidbody.velocity);
    }

    void FireWhenOutOfAmmo() {
        Debug.Log("out of ammo");
    }

    void Reload(float currentTime) {
        mLastReloadTime = currentTime;
        gun.currentAmmo = gun.ammoCapacity;
    }
}
