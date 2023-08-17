using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteract : MonoBehaviour
{
    private Image image;
    private Button button;

    void Start()
    {
        button = this.GetComponent<Button>();
        image = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!button.interactable)
        {
            image.color = Color.red;
        }
        if (button.interactable)
        {
            image.color = Color.white;
        }
    }
}
