using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Class
{
    // Class Variables
    public string name;
    public int health;
    public int speed;

    public Class(string name, int health, int speed){
        this.name = name;
        this.health = health;
        this.speed = speed;
    }
}
