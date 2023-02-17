using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private string[] _words;

    [SerializeField] private UnityEvent loaded;


    private void Start()
    {
        StartLoading();
    }

    public void StartLoading()
    {
        StartCoroutine(Waiting());
    }
    
    private IEnumerator Waiting()
    {
        foreach (var word in _words)
        {
            _text.text = word;
            _text.gameObject.GetComponent<Animation>().Play();
            yield return new WaitForSeconds(1);
        }
        loaded.Invoke();
    }
}
