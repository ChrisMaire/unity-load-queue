using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Button))]
public class Fun_Button : Fun_UIElement
{
    [Header("Components")]
    public Text label;
    public Image image;
    public Image highlightImage;
    public Button button;

    private void OnEnable()
    {
        DoTransition();
    }

    protected override void Reset()
    {
        base.Reset();

        if(label == null)
        {
            label = GetComponentInChildren<Text>();
        }

        if (image == null)
        {
            image = GetComponentInChildren<Image>();
        }

        if (button == null)
        {
            button = GetComponentInChildren<Button>();
        }
    }
}
