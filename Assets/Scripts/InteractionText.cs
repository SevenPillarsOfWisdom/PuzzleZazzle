using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;



    private void Awake()
    {
        foreach(InteractionPoint point in FindObjectsOfType<InteractionPoint>())
        {
            point.OnPlayerEnter += OnPointActivated;

            point.OnPlayerExit += OnPointDeactivated;
        }
    }

    private void OnPointActivated(string name)
    {
        _text.text = name;
    }

    private void OnPointDeactivated()
    {
        _text.text = "";
    }
}
