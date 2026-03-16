using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string correctShapeTag;
    public float correctRotationAngle = 0f;
    public static int correctPlacements = 0; 
    public int totalShapes = 3; 
    public PuzzleButton puzzleButton; 

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject != null)
        {
            DragDrop dragDrop = droppedObject.GetComponent<DragDrop>();
            if (dragDrop == null) return;

            float placedRotation = dragDrop.GetRotationAngle();
            float normalizedRotation = (placedRotation % 360 + 360) % 360;
            float normalizedCorrectRotation = (correctRotationAngle % 360 + 360) % 360;

            if (droppedObject.CompareTag(correctShapeTag) && Mathf.Approximately(normalizedRotation, normalizedCorrectRotation))
            {
                droppedObject.transform.position = transform.position;
                droppedObject.transform.rotation = Quaternion.Euler(0, 0, correctRotationAngle);
                droppedObject.GetComponent<DragDrop>().enabled = false;
                correctPlacements++;

                Debug.Log(" Correct Placement & Rotation! (" + correctPlacements + "/" + totalShapes + ")");

                if (correctPlacements >= totalShapes && puzzleButton != null)
                {
                    puzzleButton.PuzzleCompleted();
                    puzzleButton.PuzzleCompleted();
                }
            }
            else
            {
                dragDrop.ResetPosition();
                Debug.Log(" Wrong Placement or Rotation! Moving back.");
            }
        }
    }
}
