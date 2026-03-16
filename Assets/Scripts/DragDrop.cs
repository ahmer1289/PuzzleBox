using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private static DragDrop currentlyDragging = null;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (currentlyDragging == this)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                RotateLeft();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                RotateRight();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentlyDragging = this;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        currentlyDragging = null;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void RotateLeft()
    {
        rectTransform.Rotate(0, 0, 90f);
        Debug.Log(gameObject.name + " rotated left.");
    }

    public void RotateRight()
    {
        rectTransform.Rotate(0, 0, -90f);
        Debug.Log(gameObject.name + " rotated right.");
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        currentlyDragging = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Debug.Log(gameObject.name + " position & rotation reset.");
    }

    public float GetRotationAngle()
    {
        return transform.eulerAngles.z;
    }
}
