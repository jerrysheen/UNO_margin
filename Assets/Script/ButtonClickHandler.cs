using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public Button myButton;

    public GameObject buttonOrigin;
    public GameObject buttonHover;
    void Start()
    {
        myButton = GetComponent<Button>();
        buttonOrigin.SetActive(true);
        buttonHover.SetActive(false);
        // Ensure the button is assigned in the inspector
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button is not assigned in the inspector");
        }
    }

    void OnButtonClick()
    {
        // Add your button click logic here
        Debug.Log("Button clicked!");
        
    }
}
