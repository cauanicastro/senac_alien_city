using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : ScriptableObject
{
    public WEAPON id;
    public float damage;
    public Sprite weaponImage;

    public abstract void Attack(Transform firePoint);

    public void setCurrentWeapon()
    {
        Image currentWeapon = GameObject.Find("CurrentWeapon").GetComponent<Image>();
        currentWeapon.sprite = this.weaponImage;
    }
}
