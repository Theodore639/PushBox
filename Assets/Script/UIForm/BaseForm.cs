using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseForm : MonoBehaviour
{
    public virtual void Show()
    {
        transform.SetAsLastSibling();
    }

    public virtual void Hide()
    {
        transform.SetAsFirstSibling();
    }
}
