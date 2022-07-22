using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private Image kubuAUI, kubuBUI;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] Text winnerText;


    [SerializeField] GameObject unitA;
    [SerializeField] GameObject unitB;
    public Transform spawnA,spawnB;

    float timeStart;
    bool spawnBool;
    bool end;

    void Start()
    {
        end = false;
        spawnBool = false;
        timeStart = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStart >= 0)
        {
            timeStart -= Time.deltaTime;
            timerText.text = timeStart.ToString("0");
        }
        else
        {
            Destroy(startPanel);
            if (spawnBool == false)
            {
                Spawn_Unit();
                spawnBool = true;
            }
        }
        if (spawnBool == true && end == false)
        {
            float unitAHealth = GameObject.FindGameObjectWithTag("Kubu A").GetComponent<Heroes>().health;
            float unitBHealth = GameObject.FindGameObjectWithTag("Kubu B").GetComponent<Heroes>().health;
    
            kubuAUI.fillAmount = unitAHealth/100f;
            kubuBUI.fillAmount = unitBHealth/80f;

            
        }           
    }
    public void Win(string name)
    {
        end = true;
        winPanel.SetActive(true);
        winnerText.text = "Winner " + name;
    }
    private void Spawn_Unit()
    {
        Instantiate(unitA,spawnA.position,Quaternion.identity);
        Instantiate(unitB,spawnB.position,Quaternion.identity);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

}
