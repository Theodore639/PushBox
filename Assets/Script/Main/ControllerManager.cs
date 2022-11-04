using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
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

}
