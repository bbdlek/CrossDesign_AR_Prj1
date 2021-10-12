using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallInfo : MonoBehaviour
{

    Toggle _toggle;

    public int _targetScore;

    private void Start()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (PlayerPrefs.GetInt("HighScore") >= _targetScore)
            {
                _toggle.interactable = true;
            }
            else
            {
                _toggle.interactable = false;
            }
        }
        else
        {
            _toggle.interactable = false;
        }
    }
}
