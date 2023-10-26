using System.Collections;
using UnityEngine;

public class Pistol : Weapon
{
    [Header("��������������")]
    public Transform firePoint;
    public GameObject bullet;
    public int ammoCapacity;
    public float bulletSpeed;
    public float reloadTime;

    [Header("����")]
    public Transform leftHand;
    public Transform rightHand;
    public Transform leftHandPoint;
    public Transform rightHandPoint;

    public float TimeFire;
    public int ammo;
    public bool isReloading = false; // ���� ��� ������������ �������� �����������

    private void Start()
    {
        TimeFire = fireRate;
        ammo = ammoCapacity; // ��������� ���������� ���������� ��������
    }

    private void Update()
    {
        HandPosition();

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

    private void HandPosition()
    {
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateWeapon = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateWeapon + 0);

        if (leftHand != null && rightHand != null && leftHandPoint != null && rightHandPoint != null)
        {
            leftHand.position = leftHandPoint.position;
            leftHand.rotation = leftHandPoint.rotation;
            rightHand.position = rightHandPoint.position;
            rightHand.rotation = rightHandPoint.rotation;
        }
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
            gunshotSound.Play();
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
            reloadSound.Play();
            // ����� ���������� ����������� ������� ����
            StartCoroutine(CompleteReload(roundsToReload));
        }
    }

    private IEnumerator CompleteReload(int roundsToReload)
    {
        yield return new WaitForSeconds(reloadTime); // ���� ����� �����������

        ammo += roundsToReload; // ����������� ���������� �������� ����� �����������
        isReloading = false; // ���������� ���� �����������
    }
}
