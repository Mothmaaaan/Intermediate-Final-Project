using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Weapon
{
    // Weapon Variables
    public string name;
    public int damage;
    public float speed;

    public Weapon(string name, int damage, float speed){
        this.name = name;
        this.damage = damage;
        this.speed = speed;
    }
}
