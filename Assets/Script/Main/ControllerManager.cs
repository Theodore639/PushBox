using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour
{
    public GameObject color, bomb;
    public Button redoButton;
    public Transform colorRect, bombRect;

    private static ControllerManager instance;
    public static ControllerManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(ControllerManager)) as ControllerManager;
            }
            return instance;
        }
    }

    private List<Forward> commandList = new List<Forward>();

    public void RecieveCommand(int forward)
    {
        commandList.Add((Forward)forward);
    }

    public Forward GetCommand()
    {
        Forward result = Forward.None;
        if (commandList.Count > 0)
        {
            result = commandList[0];
            commandList.RemoveAt(0);
        }
        return result;
    }

    public void OnColorClick()
    {

    }

    public void OnColorDragEnd()
    {
        color.transform.position = Vector3.zero;
    }

    public void OnBombClick()
    {

    }

    public void OnBombDragEnd()
    {
        bomb.transform.position = Vector3.zero;
    }

    public void OnRedoClick()
    {

    }

    public void SetRedoActive(bool isActive)
    {
        redoButton.interactable = isActive;
        redoButton.transform.Find("Redo").GetComponent<Image>().color = new Color(1, 1, 1, isActive ? 1 : 0.5f);
    }

}
