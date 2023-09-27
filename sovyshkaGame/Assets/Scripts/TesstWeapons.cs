using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TesstWeapons : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPosition;

    public float StartTimeFire;
    public float damage;

    private float TimeFire;


    // Start is called before the first frame update
    void Start()
    {
        TimeFire = StartTimeFire;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            Debug.Log("k");
            if (TimeFire <= 0)
            {
                GameObject gobj = Instantiate(bullet, bulletPosition.position, transform.rotation);
                gobj.GetComponent<TestBullet>().setWeapon(this);
                TimeFire = StartTimeFire;
            }
            else {
                TimeFire -= Time.deltaTime;
            }
        }
    }

    public float getWeaponDamage() {
        return damage;
    }

}
