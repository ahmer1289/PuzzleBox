using UnityEngine;
using TMPro;

public class PuzzleButton : MonoBehaviour
{
    public GameObject puzzleCanvas;
    public Transform playerCamera;
    public Animator doorAnimator;
    public AudioSource doorSound;

    private bool isSolved = false;
    public static int solvedCount = 0;
    private static int totalPuzzles = 2;

    [Header("Timer Settings")]
    public float puzzleTimeLimit = 10f;
    private float timeRemaining;
    private bool isTimerRunning = false;
    public TextMeshProUGUI timerText;
    private bool playerIsUsingPuzzle = false;

    [Header("Puzzle Reset")]
    public DragDrop[] puzzlePieces;

    void Start()
    {
        if (puzzleCanvas != null)
        {
            puzzleCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (puzzleCanvas == null || playerCamera == null) return;

        float distance = Vector3.Distance(transform.position, playerCamera.position);

        if (distance < 3f && Input.GetKeyDown(KeyCode.E) && !isSolved)
        {
            puzzleCanvas.SetActive(true);
            playerIsUsingPuzzle = true;
            StartTimer();
            Debug.Log("Puzzle Canvas Opened!");
        }

        if (playerIsUsingPuzzle && distance > 3f)
        {
            ResetPuzzle();
            puzzleCanvas.SetActive(false);
            playerIsUsingPuzzle = false;
        }

        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                ResetPuzzle();
            }
        }
    }

    void StartTimer()
    {
        timeRemaining = puzzleTimeLimit;
        isTimerRunning = true;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time Left: " + Mathf.Ceil(timeRemaining) + "s";
        }
    }

    void ResetPuzzle()
    {
        isTimerRunning = false;
        timeRemaining = puzzleTimeLimit; 

        DropZone.correctPlacements = 0;

        foreach (DragDrop piece in puzzlePieces)
        {
            piece.ResetPosition();
            piece.enabled = true;
        }
        StartTimer();
    }



    public void PuzzleCompleted()
    {
        if (!isSolved)
        {
            isSolved = true;
            isTimerRunning = false;
            solvedCount++;
            puzzleCanvas.SetActive(false);

            Debug.Log("Puzzle Solved! Total solved: " + solvedCount);

            if (solvedCount == totalPuzzles)
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        if (doorSound != null && !doorSound.isPlaying)
        {
            doorSound.Play();
        }

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("OpenDoor");
        }
    }
}
