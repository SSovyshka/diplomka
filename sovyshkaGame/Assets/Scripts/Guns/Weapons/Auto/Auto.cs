using System.Collections;
using UnityEngine;

public class Auto : Weapon
{
    public Transform firePoint;
    public GameObject bullet;
    public int ammoCapacity;
    public float bulletSpeed;
    public float reloadTime;

    private float TimeFire;
    private int ammo;
    private bool isReloading = false; // ���� ��� ������������ �������� �����������


    private void Start()
    {
        TimeFire = fireRate;
        ammo = ammoCapacity; // ��������� ���������� ���������� ��������
    }

    private void Update()
    {
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateWeapon = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f, rotateWeapon + 0);

        if (Input.GetKey(KeyCode.Mouse0) && !isReloading && ammo > 0)
        {
            Shoot();
            Debug.Log(ammo);
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammo < ammoCapacity)
        {
            Reload();
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
            ammo--; // ���������� ���������� ��������� �������� ��� ��������
        }
    }

    public override void Reload()
    {
        int roundsToReload = ammoCapacity - ammo; // ������������, ������� �������� ����� ��������

        if (roundsToReload >= 0)
        {
            isReloading = true; // ������������� ���� �����������
            // ����� ����� ������������� ���� ����������� ��� ��������
            // ����� ���������� ����������� ������� ����
            StartCoroutine(CompleteReload(roundsToReload));
        }
    }

    private IEnumerator CompleteReload(int roundsToReload)
    {
        playReloadSound();
        yield return new WaitForSeconds(reloadTime); // ���� ����� �����������

        ammo += roundsToReload; // ����������� ���������� �������� ����� �����������
        isReloading = false; // ���������� ���� �����������
    }

    public void playGunSound()
    {
        gunshotSound.Play();
    }
    public void playReloadSound()
    {
        reloadSound.Play();
    }
}
