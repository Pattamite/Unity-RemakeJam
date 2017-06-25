using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuSelector : MonoBehaviour {

    public Button[] button;
    private int currentSelect;
    private bool isMouseUsed;

    void Start()
    {
        currentSelect = -1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouse();

        if (currentSelect != -1 && isMouseUsed)
        {
            currentSelect = -1;
            ClearSelect();
        }
        else KeyboardSelect();
    }

    private void CheckMouse()
    {
        isMouseUsed = false;
        for (int i = 0 ; i < button.Length ; i++)
        {
            //print(button[i] + " / " + button[i].gameObject.GetComponent<MouseDetector>().isMouseSelected);
            if (button[i].gameObject.GetComponent<MouseDetector>().isMouseSelected)
            {
                isMouseUsed = true;
                break;
            }
        }
    }

    private void ClearSelect()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }

    private void KeyboardSelect()
    {
         if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (currentSelect == -1)
            {
                currentSelect = 0;
            }
            else
            {
                ClearSelect();
                currentSelect = (currentSelect + 1) % button.Length;
            }
            button[currentSelect].Select();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (currentSelect == -1)
            {
                currentSelect = button.Length - 1;
            }
            else
            {
                ClearSelect();
                currentSelect = (currentSelect - 1 + button.Length) % button.Length;
            }
            button[currentSelect].Select();
        }
    }
}
