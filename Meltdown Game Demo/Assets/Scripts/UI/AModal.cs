using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AModal : MonoBehaviour
{
    [Header("Modal Components")]
    [SerializeField] GameObject _modalContainer;

    public virtual void DisplayModal(bool show)
    {
        _modalContainer.SetActive(show);
    }
}
