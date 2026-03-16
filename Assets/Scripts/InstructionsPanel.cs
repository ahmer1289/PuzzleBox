using UnityEngine;

public class InstructionPanel : MonoBehaviour
{
    public GameObject room1Instructions;
    public GameObject room2Instructions;
    private bool isInRoom2 = false; 

    void Start()
    {
        room1Instructions.SetActive(true); 
        room2Instructions.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isInRoom2)
            {
                TogglePanel(room2Instructions);
            }
            else
            {
                TogglePanel(room1Instructions);
            }
        }
    }

    void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void EnterRoom2()
    {
        isInRoom2 = true;
        room1Instructions.SetActive(false);
        room2Instructions.SetActive(true);
    }
}
