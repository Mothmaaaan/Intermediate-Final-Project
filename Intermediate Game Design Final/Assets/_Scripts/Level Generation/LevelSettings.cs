using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettings : MonoBehaviour
{
    [Header("Environment Settings")]
    public int levelSize;
    public int obstacleDensity;
    public int obstacleSize;
    public string mode;
    public bool crazyObstacles;

    [Header("Sliders")]
    public Slider mapSizeSlider;
    public Slider obstacleDensitySlider;
    public Slider obstacleSizeSlider;
    public Toggle modeToggle;
    public Toggle crazyToggle;

    [Header("Player Settings")]
    public TMP_InputField playerNameInput;
    public TMP_Dropdown classDropdown;


    private void Awake() {
        LoadLevelSettings();

        levelSize = Mathf.RoundToInt(mapSizeSlider.value);
        obstacleDensity = Mathf.RoundToInt(obstacleDensitySlider.value);
        obstacleSize = Mathf.RoundToInt(obstacleSizeSlider.value);

        if(modeToggle.isOn){
            mode = "Dark";
        }else{
            mode = "Light";
        }

        if(crazyToggle.isOn){
            crazyObstacles = true;
        }else{
            crazyObstacles = false;
        }
    }

    public void SetLevelSize(Slider slider){
        levelSize = Mathf.RoundToInt(slider.value);
    }

    public void SetLevelSizeText(TextMeshProUGUI text){
        text.text = levelSize.ToString();
    }

    public void SetObstacleDensity(Slider slider){
        obstacleDensity = Mathf.RoundToInt(slider.value);
    }

    public void SetObstacleDensityText(TextMeshProUGUI text){
        text.text = obstacleDensity.ToString();
    }
    
    public void SetObstacleSize(Slider slider){
        obstacleSize = Mathf.RoundToInt(slider.value);
    }

    public void SetObstacleSizeText(TextMeshProUGUI text){
        text.text = obstacleSize.ToString();
    }

    public void ToggleMode(Toggle toggle){
        if(toggle.isOn){
            mode = "Dark";
        }else{
            mode = "Light";
        }
    }

    public void ToggleCrazy(Toggle toggle){
        if(toggle.isOn){
            crazyObstacles = true;
        }else{
            crazyObstacles = false;
        }
    }

#region Saving and Loading
// Save our level settings.
    public void SaveLevelSettings(){
        PlayerPrefs.SetInt("_mapsize", Mathf.RoundToInt(mapSizeSlider.value));
        PlayerPrefs.SetInt("_obstacledensity", Mathf.RoundToInt(obstacleDensitySlider.value));
        PlayerPrefs.SetInt("_obstaclesize", Mathf.RoundToInt(obstacleSizeSlider.value));

        PlayerPrefs.SetString("_playername", playerNameInput.text);
        PlayerPrefs.SetInt("_class", classDropdown.value);

        if(modeToggle.isOn)
            PlayerPrefs.SetInt("_mode", 1);
        else
            PlayerPrefs.SetInt("_mode", 0);

        if(crazyToggle.isOn)
            PlayerPrefs.SetInt("_crazy", 1);
        else
            PlayerPrefs.SetInt("_crazy", 0);
    }

// Load our level settings.
    public void LoadLevelSettings(){
        mapSizeSlider.value = PlayerPrefs.GetInt("_mapsize");
        obstacleDensitySlider.value = PlayerPrefs.GetInt("_obstacledensity");
        obstacleSizeSlider.value = PlayerPrefs.GetInt("_obstaclesize");

        playerNameInput.text = PlayerPrefs.GetString("_playername");
        classDropdown.value = PlayerPrefs.GetInt("_class");

        if(PlayerPrefs.GetInt("_mode") == 1)
            modeToggle.SetIsOnWithoutNotify(true);
        else
            modeToggle.SetIsOnWithoutNotify(false);

        if(PlayerPrefs.GetInt("_crazy") == 1)
            crazyToggle.SetIsOnWithoutNotify(true);
        else
            crazyToggle.SetIsOnWithoutNotify(false);
    }
#endregion
}
