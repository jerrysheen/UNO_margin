using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonInteractionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // This method is called when the pointer enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Add your hover logic here
        Debug.Log("Pointer entered button area!");
    }

    // This method is called when the pointer exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        // Add your hover exit logic here
        Debug.Log("Pointer exited button area!");
    }

    // This method is called when the pointer clicks on the button
    public void OnPointerClick(PointerEventData eventData)
    {
        // Add your click logic here
        Debug.Log("Button clicked!");
    }
}