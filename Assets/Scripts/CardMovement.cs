using UnityEngine;
using UnityEngine.EventSystems; // This allows us to use Unity's event system to detect our mouse inputs

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler // These classes hold the methods required to handle UI interactions that we need
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private Vector3 originalPosition;
    private int currentState; // 0: Normal, 1: Hovered, 2: Dragged

    [SerializeField] private float selectScale = 1.1f; // Scale when hovered
    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector3 playPosition;
    [SerializeField] private GameObject glowEffect;
    [SerializeField] private GameObject playArrow;
    [SerializeField] private float lerpFactor = 0.1f; // Offset for play position

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); // Get the RectTransform component of the attached GameObject
        canvas = GetComponentInParent<Canvas>(); // Get the Canvas component of the attached GameObject
        originalScale = rectTransform.localScale;
        originalRotation = rectTransform.localRotation;
        originalPosition = rectTransform.localPosition;
    }

    void Update()
    {
        switch (currentState)
        {
            case 1: // Hovered state
                HandleHoverState();
                break;
            case 2: // Dragged state
                HandleDragState();
                if (!Input.GetMouseButton(0)) // If not dragging, return to normal state
                {
                    TransitionToState0();
                }
                break;
            case 3: // Play state
                HandlePlayState();
                if (!Input.GetMouseButton(0)) // If not dragging, return to normal state
                {
                    TransitionToState0();
                }
                break;
        }
    }

    private void TransitionToState0()
    {
        currentState = 0;
        rectTransform.localScale = originalScale;
        rectTransform.localRotation = originalRotation;
        rectTransform.localPosition = originalPosition;
        glowEffect.SetActive(false);
        playArrow.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData) // This is inherited from the IPointerEnterHandler class referenced above
    {
        if (currentState == 0) // Only change state if currently in normal state
        {
            originalPosition = rectTransform.localPosition; // Store the original position
            originalRotation = rectTransform.localRotation; // Store the original rotation
            originalScale = rectTransform.localScale; // Store the original scale

            currentState = 1; // Transition to hovered state
        }
    }

    public void OnPointerExit(PointerEventData eventData) // This is inherited from the IPointerExitHandler class referenced above
    {
        if (currentState == 1) // Only change state if currently in hovered state
        {
            TransitionToState0(); // Transition back to normal state
        }
    }

    public void OnPointerDown(PointerEventData eventData) // This is inherited from the IPointerDownHandler class referenced above
    {
        if (currentState == 1) // Only change state if currently in hovered state
        {
            currentState = 2; // Transition to dragged state
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(),
                eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition); // Using the event system to detect what is clicked on
            originalPanelLocalPosition = rectTransform.localPosition;
        }
    }

    public void OnDrag(PointerEventData eventData) // This is inherited from the IDragHandler class referenced above
    {
        if (currentState == 2) // Only handle drag if in dragged state
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(),
                eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFactor);

                if (rectTransform.localPosition.y > cardPlay.y)
                {
                    currentState = 3; // Transition to play state
                    playArrow.SetActive(true);
                    rectTransform.localPosition = Vector3.Lerp(rectTransform.position, playPosition, lerpFactor); // Set position for play state
                }
            }
        }
    }

    private void HandleHoverState()
    {
        glowEffect.SetActive(true);
        rectTransform.localScale = originalScale * selectScale; // Scale up the card
    }

    private void HandleDragState()
    {
        rectTransform.localRotation = Quaternion.identity; // Reset rotation to identity
    }

    private void HandlePlayState()
    {
        rectTransform.localRotation = Quaternion.identity; // Reset rotation to identity
        rectTransform.localPosition = playPosition; // Set position for play state

        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2; // Transition back to dragged state if mouse is below the play position
            playArrow.SetActive(false);
        }
    }
}
