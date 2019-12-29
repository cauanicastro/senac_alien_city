using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Melee", order = 2)]
public class MeleeScOb : Weapon
{
    public override void Attack(Transform firePoint)
    {
        //Do nothing
        Debug.Log("melee weapon");
    }
}
