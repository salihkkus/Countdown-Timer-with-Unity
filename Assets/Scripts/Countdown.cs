using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Image timerImg;
    [SerializeField] private Text timerText;
    [SerializeField] private InputField inputField; // Kullanıcının süre gireceği alan
    [SerializeField] private Button startButton; // Zamanlayıcıyı başlatan buton
    [SerializeField] private float duration;
    [SerializeField] private float currenttime;
    
    void Start()
    {
        startButton.onClick.AddListener(StartCountdown);
    }

    private void StartCountdown()
    {
        if (float.TryParse(inputField.text, out duration) && duration > 0) // Kullanıcının girdisini al
        {
            currenttime = duration;
            timerText.text = currenttime.ToString();
            StartCoroutine(UpdateTime());
        }
        else
        {
            Debug.Log("Geçerli bir süre giriniz!");
        }
    }

    private IEnumerator UpdateTime()
    {
        while (currenttime >= 0)
        {
            timerImg.fillAmount = Mathf.InverseLerp(0, duration, currenttime);
            timerText.text = currenttime.ToString();
            yield return new WaitForSeconds(1f);
            currenttime--;
        }
    }
}
