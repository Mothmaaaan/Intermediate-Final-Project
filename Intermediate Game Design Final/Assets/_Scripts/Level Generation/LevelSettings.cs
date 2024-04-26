using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettings : MonoBehaviour
{
    [Header("Environment Settings")]
    public int levelSize;
    public int obstacleDensity;
    public int obstacelSize;

    


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
        obstacelSize = Mathf.RoundToInt(slider.value);
    }

    public void SetObstacleSizeText(TextMeshProUGUI text){
        text.text = obstacelSize.ToString();
    }
}
