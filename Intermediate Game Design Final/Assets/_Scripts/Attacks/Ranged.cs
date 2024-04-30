using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged
{
    // Attack Variables
    public string aName;
    public float damage;
    public float cooldown;
    public float rangedRadius;

    public Ranged(string name, float damage, float cooldown, float rangedRadius){
        this.aName = name;
        this.damage = damage;
        this.cooldown = cooldown;
        this.rangedRadius = rangedRadius;
    }
}
