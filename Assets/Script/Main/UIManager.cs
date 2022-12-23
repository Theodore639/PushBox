using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIManager
{
    static string uiFormPath = "Prefabs/UIForm/";

    public static void ShowUIForm(Type type)
    {
        GameObject.Instantiate(Resources.Load(uiFormPath + type.Name));
    }
}
