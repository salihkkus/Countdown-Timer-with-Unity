using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Image timerImg; // UI image representing the timer's progress
    [SerializeField] private Text timerText; // Text displaying the remaining time
    [SerializeField] private InputField inputField; // Input field where the user enters the countdown time
    [SerializeField] private Button startButton; // Button to start the countdown
    [SerializeField] private float duration; // Total duration of the countdown
    [SerializeField] private float currenttime; // Current time left in the countdown
    
    void Start()
    {
        // Assign the StartCountdown method to the button click event
        startButton.onClick.AddListener(StartCountdown);
    }

    private void StartCountdown()
    {
        // Get the user input, convert it to a float, and check if it's valid
        if (float.TryParse(inputField.text, out duration) && duration > 0) 
        {
            currenttime = duration; // Initialize the timer
            timerText.text = currenttime.ToString(); // Update the timer text
            StartCoroutine(UpdateTime()); // Start the countdown coroutine
        }
        else
        {
            Debug.Log("Please enter a valid time!"); // Show an error message if the input is invalid
        }
    }

    private IEnumerator UpdateTime()
    {
        while (currenttime >= 0)
        {
            // Update the UI image fill amount to reflect the countdown progress
            timerImg.fillAmount = Mathf.InverseLerp(0, duration, currenttime);
            
            // Update the displayed countdown time
            timerText.text = currenttime.ToString();
            
            yield return new WaitForSeconds(1f); // Wait for one second before decreasing the time
            
            currenttime--; // Decrease the countdown time
        }
    }
}
