using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UIControllable : MonoBehaviour
{
    [Serializable]
    protected struct UIElement
    {
        public GameObject GameObject;
        public string Name;
    }

    [SerializeField] protected UIElement[] _elements;
    [SerializeField] private AudioClip _clickSound;

    public void ShowElement(string name)
    {
        AudioManager.getInstance().PlayAudio(_clickSound);
        foreach (var element in _elements)
            element.GameObject.SetActive(element.Name.Equals(name));
    }

    public void ShowElement(int id)
    {
        AudioManager.getInstance().PlayAudio(_clickSound);
        foreach (var element in _elements)
            element.GameObject.SetActive(false);
        _elements[id].GameObject.SetActive(true);
    }
}
