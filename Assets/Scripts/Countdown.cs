using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    [SerializeField] private Image timerImg;
    [SerializeField] private Text timerText;
    [SerializeField] private float duration;
    [SerializeField] private float currenttime;

    
    void Start()
    {
        currenttime = duration;
        timerText.text = currenttime.ToString();
        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
       while(currenttime >= 0)
       {
        timerImg.fillAmount = Mathf.InverseLerp(0 , duration , currenttime);
        timerText.text = currenttime.ToString();
        yield  return new WaitForSeconds(1f);
        currenttime--;
       }

       yield return null;
    }

    
}
