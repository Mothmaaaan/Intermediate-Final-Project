using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Melee
{
    // Attack Variables
    public string aName;
    public float damage;
    public float cooldown;
    public float length;
    public float radius;
    public Color color;

    public Melee(string name, float damage, float cooldown, float length, float radius, Color color){
        this.aName = name;
        this.damage = damage;
        this.cooldown = cooldown;
        this.length = length;
        this.radius = radius;
        this.color = color;
    }
}
