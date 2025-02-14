using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Image timerImg;
    [SerializeField] private Text timerText;
    [SerializeField] private InputField inputField;
    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton; // Pause butonu
    [SerializeField] private AudioSource alarmSound;
    [SerializeField] private float duration;
    private float currenttime;
    
    private Coroutine countdownCoroutine;
    private bool isPaused = false; // Pause durumu kontrolü

    void Start()
    {
        startButton.onClick.AddListener(StartCountdown);
        stopButton.onClick.AddListener(TogglePause); // Pause butonu
        stopButton.gameObject.SetActive(true);
        alarmSound.Stop();
    }

    private void StartCountdown()
    {
        if (float.TryParse(inputField.text, out duration) && duration > 0)
        {
            if (countdownCoroutine != null)
                StopCoroutine(countdownCoroutine);

            currenttime = duration;
            timerText.text = currenttime.ToString();
            countdownCoroutine = StartCoroutine(UpdateTime());
            isPaused = false; // Pause açıldığında devam etsin
            alarmSound.Stop();
        }
        else
        {
            Debug.Log("Please enter a valid time!");
        }
    }

    private IEnumerator UpdateTime()
    {
        while (currenttime >= 0)
        {
            if (!isPaused) // Eğer duraklatılmışsa bekle
            {
                timerImg.fillAmount = Mathf.InverseLerp(0, duration, currenttime);
                timerText.text = currenttime.ToString();
                yield return new WaitForSeconds(1f);
                currenttime--;
            }
            else
            {
                yield return null; // Pause durumunda bekle
            }
        }

        TimerFinished();
    }

    private void TimerFinished()
    {
        Debug.Log("Time's up! Alarm is ringing!");
        alarmSound.Play();
    }

    private void TogglePause()
    {
        isPaused = !isPaused; // Pause durumunu değiştir
        if (isPaused)
        {
            Debug.Log("Countdown paused!");
            alarmSound.Stop(); // PAUSE OLUNCA ALARM SESSİZ OLSUN
        }
        else
        {
            Debug.Log("Countdown resumed!");
        }
    }
}
