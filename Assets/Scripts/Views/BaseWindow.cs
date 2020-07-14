using UnityEngine;
using System.Collections;

public abstract class BaseWindow : MonoBehaviour, IWindow
{
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
}
