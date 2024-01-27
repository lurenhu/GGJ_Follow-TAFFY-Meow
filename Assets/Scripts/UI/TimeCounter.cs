using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{

    public GameObject minText;
    public GameObject secText;
    public GameObject Lose;
    public int totalTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timeCounter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator timeCounter()
    {
        while (totalTime > 0) 
        {
            minText.GetComponent<Text>().text = (totalTime / 60).ToString();
            secText.GetComponent<Text>().text = (totalTime % 60).ToString();
            yield return new WaitForSeconds(1f);
            totalTime--;
            if (totalTime == 0) 
            {
                PauseMenu.gameIsPause = true;
                Time.timeScale = 0;
                Lose.SetActive(true);
            }

        }
    }
}
