using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float fireRate;
    public float accuracy;

    public AudioSource gunshotSound;
    public AudioSource reloadSound;

    public float getDamage() { 
        return damage;
    }

    public virtual void Shoot(){

    }

    public virtual void Reload() { 
        
    }

}
