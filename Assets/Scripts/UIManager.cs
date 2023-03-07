using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Button _getCoinsButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    

    public void ActivateGetCoinsButton()
    {
        _getCoinsButton.gameObject.SetActive(true);
    }

    public void DectivateGetCoinsButton()
    {
        _getCoinsButton.gameObject.SetActive(false);
    }


}
