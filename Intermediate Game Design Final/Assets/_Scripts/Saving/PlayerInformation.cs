using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public TMP_InputField iField;
    private string _dataPath;
    private string _textFile;

    private void Start() {
        try{
            LoadPlayerInformation();
        }catch(FileNotFoundException ex){
            Debug.Log("Load error: " + ex.ToString());
        }
        
    }

    public void SavePlayerInformation(){
        // SAVING TO UNITY ASSETS FOR NOW FOR EASY DEBUGGING AND READABILITY!
        _dataPath = "Assets/";
        //Debug.Log(_dataPath);
        _textFile = _dataPath + "PlayerSaveData.txt";

        if(File.Exists(_textFile)){
            //Debug.Log("File already exists!");
        }

        File.WriteAllText(_textFile, iField.text);
        //Debug.Log("File created!");
    }

    public void LoadPlayerInformation(){
        iField.text = File.ReadAllText("Assets/PlayerSaveData.txt");
    }
}
