using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using System.Runtime.CompilerServices;

[Serializable]
public class WeaponList : MonoBehaviour
{
    // Weapons
    public Weapon BrokenSword = new Weapon("Broken Sword", 5, 1);
    public Weapon Sword = new Weapon("Sword", 15, 2);
    public Weapon BladeOfGrass = new Weapon("Blade of Grass", 10, 3);
    public Weapon Axe = new Weapon("Axe", 30, .5f);

    // Weapon List
    public List<Weapon> weapons = new List<Weapon>();


    private void Awake() {
        weapons.Add(BrokenSword);
        weapons.Add(Sword);
        weapons.Add(BladeOfGrass);
        weapons.Add(Axe);
    }

#region Add Class
    // Return the class that has the same name as className.
    public Weapon SearchForClass(string weaponName){
        
        for(int i=0; i<weapons.Count; i++){
            if(weapons[i].name == weaponName)
                return weapons[i];
        }

        // Return the first class if we cannot find what class we are searching for.
        return weapons[0];
    }
#endregion

#region Saving and Loading Weapons
// Save the weapon list to a JSON file.
    public void SaveWeapons(){
        /*WeaponsInInventory wii = new WeaponsInInventory
        {
            inventory = weapons
        };
        //print(wii.inventory[0].name);

        string jsonString = JsonUtility.ToJson(wii, true);

        using(StreamWriter stream = File.CreateText("Assets/WeaponList2.json")){
            stream.WriteLine(jsonString);
        }*/

        WeaponsInInventory wii = new WeaponsInInventory();
        wii.inventory = weapons;

        string jsonString = JsonUtility.ToJson(wii, true);

        using(StreamWriter stream = File.CreateText("Assets/WeaponList.json")){
            stream.WriteLine(jsonString);
        }

    }

#endregion
}
