using System.Collections;
using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    private Animator anim;
    private bool IsAtDoor = false;
    private bool doorOpened = false;

    [SerializeField] private TextMeshProUGUI CodeText;
    private string codeTextValue = "";
    public string safeCode;
    public GameObject CodePanel;
    public AudioSource doorSound;
    private PlayerMovement playerMovement;


    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = FindObjectOfType<PlayerMovement>();


    }

    void Update()
    {
        CodeText.text = codeTextValue;

        if (!doorOpened && codeTextValue == safeCode) 
        {
            StartCoroutine(CorrectCodeRoutine());
        }
        else if (codeTextValue.Length >= 4 && !doorOpened)
        {
            StartCoroutine(WrongCodeRoutine());
        }

        if (Input.GetKey(KeyCode.E) && IsAtDoor)
        {
            CodePanel.SetActive(true);
            playerMovement.StartUsingKeypad();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsAtDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IsAtDoor = false;
        CodePanel.SetActive(false);
        playerMovement.StopUsingKeypad();
    }

    public void AddDigit(string digit)
    {
        codeTextValue += digit;
    }

    public void ClearCode()
    {
        codeTextValue = "";
    }

    private IEnumerator CorrectCodeRoutine()
    {
        doorOpened = true;
        CodeText.text = "ACCESS GRANTED";
        yield return new WaitForSeconds(2f);
        playerMovement.StopUsingKeypad();
        if (doorSound != null && !doorSound.isPlaying)
        {
            doorSound.Play();
        }
        anim.SetTrigger("OpenDoor");

        CodePanel.SetActive(false);
        codeTextValue = "";
        CodeText.text = "";
    }


    private IEnumerator WrongCodeRoutine()
    {
        CodeText.text = "ACCESS DENIED";
        yield return new WaitForSeconds(1f);
        codeTextValue = "";
        CodeText.text = codeTextValue;
    }

    public void CloseKeypad()
    {
        CodePanel.SetActive(false);
        playerMovement.StopUsingKeypad();
    }
}
