using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    bool isStartTimer = false;
    public Slider timerSlider;
    public Image FillImage;
    float totalTime;
    float reduceValue;
    float colorChangeTime;

    void Start()
    {
        // sliderのmaxValue秒ゲームをする
        totalTime = timerSlider.maxValue;
        timerSlider.value = totalTime;
        colorChangeTime = totalTime / 3;
    }

    void Update()
    {
        if(isStartTimer)
        {
            timerSlider.value -= Time.deltaTime;
            if(timerSlider.value <= colorChangeTime)
            {
                FillImage.color = new Color32(255,0,0,255);;
            }
            if(timerSlider.value <= 0)
            {
                //Debug.Log("Time Up!");
                gameManager.finishGame();
                isStartTimer = false;
            }
        }
        
    }
    public void onTimerStart()
    {
        isStartTimer = true;
    }
}
