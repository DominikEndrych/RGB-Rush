using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    private bool isPressed = false;
    private Image image;

    [SerializeField] Color pressedColor;
    [SerializeField] Color disabledColor;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ChangeColor);
        image = gameObject.GetComponent<Image>();
        image.color = disabledColor;
    }

    public void ChangeColor()
    {
        isPressed = !isPressed;

        if (isPressed)
        {
            image.color = pressedColor;
        }
        else
        {
            image.color = disabledColor;
        }
        
    }
}
