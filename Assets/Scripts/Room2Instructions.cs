using UnityEngine;

public class Room2Instructions : MonoBehaviour
{
    public InstructionPanel instructionPanel; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            instructionPanel.EnterRoom2(); 
        }
    }
}
