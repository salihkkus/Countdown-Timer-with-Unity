using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Image timerImg; // UI image representing the timer's progress
    [SerializeField] private Text timerText; // Text displaying the remaining time
    [SerializeField] private InputField inputField; // Input field where the user enters the countdown time
    [SerializeField] private Button startButton; // Button to start the countdown
    [SerializeField] private Button stopButton; // Pause button to pause/resume the countdown
    [SerializeField] private AudioSource alarmSound; // Audio source for the alarm sound
    [SerializeField] private float duration; // Total duration of the countdown
    private float currenttime; // Current time left in the countdown
    
    private Coroutine countdownCoroutine; // Coroutine reference to control the countdown
    private bool isPaused = false; // Flag to track whether the countdown is paused

    void Start()
    {
        // Assign methods to button click events
        startButton.onClick.AddListener(StartCountdown);
        stopButton.onClick.AddListener(TogglePause);

        stopButton.gameObject.SetActive(true); // Ensure the pause button is always visible
        alarmSound.Stop(); // Ensure the alarm is stopped at the start
    }

    private void StartCountdown()
    {
        // Parse the user input and validate the duration
        if (float.TryParse(inputField.text, out duration) && duration > 0)
        {
            // Stop any ongoing countdown before starting a new one
            if (countdownCoroutine != null)
                StopCoroutine(countdownCoroutine);

            currenttime = duration; // Initialize countdown time
            timerText.text = currenttime.ToString(); // Update the displayed time
            countdownCoroutine = StartCoroutine(UpdateTime()); // Start the countdown coroutine
            isPaused = false; // Reset the pause flag when starting
            alarmSound.Stop(); // Stop alarm if it was previously playing
        }
        else
        {
            Debug.Log("Please enter a valid time!"); // Display an error message if input is invalid
        }
    }

    private IEnumerator UpdateTime()
    {
        while (currenttime >= 0)
        {
            if (!isPaused) // Only update the timer if it is not paused
            {
                timerImg.fillAmount = Mathf.InverseLerp(0, duration, currenttime); // Update the UI fill
                timerText.text = currenttime.ToString(); // Update the displayed time
                yield return new WaitForSeconds(1f); // Wait for one second
                currenttime--; // Decrease the countdown time
            }
            else
            {
                yield return null; // If paused, just wait
            }
        }

        TimerFinished(); // Trigger alarm when time reaches zero
    }

    private void TimerFinished()
    {
        Debug.Log("Time's up! Alarm is ringing!");
        alarmSound.Play(); // Play the alarm sound when countdown finishes
    }

    private void TogglePause()
    {
        isPaused = !isPaused; // Toggle the pause state

        if (isPaused)
        {
            Debug.Log("Countdown paused!");
            alarmSound.Stop(); // Stop the alarm sound when paused
        }
        else
        {
            Debug.Log("Countdown resumed!");
        }
    }
}
