using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private Image kubuAUI, kubuBUI;


    float timeStart;

    void Start()
    {
    
        timeStart = 3;
    }

    // Update is called once per frame
    void Update()
    {
        float unitAHealth = GameObject.FindGameObjectWithTag("Kubu A").GetComponent<Heroes>().health;
        float unitBHealth = GameObject.FindGameObjectWithTag("Kubu B").GetComponent<Heroes>().health;
    
        if (timeStart >= 0)
        {
            timeStart -= Time.deltaTime;
            timerText.text = timeStart.ToString("0");
        }
        kubuAUI.fillAmount = unitAHealth/100f;
        kubuBUI.fillAmount = unitBHealth/80f;
    }
}
