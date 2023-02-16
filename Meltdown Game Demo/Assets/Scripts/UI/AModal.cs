using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AModal : MonoBehaviour
{
    [Header("Modal Components")]
    [SerializeField] GameObject _modalContainer;

    public bool IsShowing { get { return _modalContainer.activeInHierarchy; } }

    public virtual void DisplayModal(bool show)
    {
        _modalContainer.SetActive(show);
    }
}
