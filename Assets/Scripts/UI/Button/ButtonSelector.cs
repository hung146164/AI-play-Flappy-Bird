using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonSelector : MonoBehaviour
{
    public List<Button> buttons;
    private Button selectedButton;

    void Start()
    {
        foreach (var btn in buttons)
        {
            btn.onClick.AddListener(() => OnButtonClicked(btn));
        }

        if (buttons.Count > 0)
            OnButtonClicked(buttons[0]); 
    }

    void OnButtonClicked(Button clickedButton)
    {
        selectedButton = clickedButton;

        foreach (var btn in buttons)
        {
            bool isSelected = btn == clickedButton;
            var colors = btn.colors;
            colors.normalColor = isSelected ? Color.green : Color.white;
            colors.selectedColor = isSelected ? Color.green : Color.white;
            btn.colors = colors;
        }
    }
}
