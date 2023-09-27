using UnityEngine;

public class Auto : Weapon
{
    public Transform firePoint;
    public GameObject bullet;

    public int ammoCapacity;
    public float bulletSpeed;
    private float TimeFire;

    private void Start()
    {
        TimeFire = fireRate;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
        TimeFire -= Time.deltaTime;
    }


    public override void Shoot()
    {
        if (TimeFire <= 0)
        {
            GameObject gObject = Instantiate(bullet, firePoint.position, firePoint.rotation);
            if (gObject != null)
            {
                gObject.GetComponent<Bullet>().weapon = this;
                gObject.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
            }
            playGunSound();
            TimeFire = fireRate;
        }
    }

    public void playGunSound()
    {
        gunshotSound.Play();
    }

}
