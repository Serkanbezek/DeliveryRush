using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourierManager : MonoBehaviour
{
    private int _currentCourier;

    private void OnEnable()
    {
        ShopManager.CourierChanged += ChangeCourier;
    }

    private void Start()
    {
        ChangeCourier(PlayerPrefs.GetInt("ActiveCourier"));
    }

    private void OnDisable()
    {
        ShopManager.CourierChanged -= ChangeCourier;
    }

    private void ChangeCourier(int activeCourier)
    {
        switch (activeCourier)
        {
            case 0:
                transform.GetChild(_currentCourier).transform.gameObject.SetActive(false);
                transform.GetChild(0).transform.gameObject.SetActive(true);
                _currentCourier = 0;
                break;
            case 1:
                transform.GetChild(_currentCourier).transform.gameObject.SetActive(false);
                transform.GetChild(1).transform.gameObject.SetActive(true);
                _currentCourier = 1;
                break;
            case 2:
                transform.GetChild(_currentCourier).transform.gameObject.SetActive(false);
                transform.GetChild(2).transform.gameObject.SetActive(true);
                _currentCourier = 2;
                break;
            case 3:
                transform.GetChild(_currentCourier).transform.gameObject.SetActive(false);
                transform.GetChild(3).transform.gameObject.SetActive(true);
                _currentCourier = 3;
                break;
            case 4:
                transform.GetChild(_currentCourier).transform.gameObject.SetActive(false);
                transform.GetChild(4).transform.gameObject.SetActive(true);
                _currentCourier = 4;
                break;
            case 5:
                transform.GetChild(_currentCourier).transform.gameObject.SetActive(false);
                transform.GetChild(5).transform.gameObject.SetActive(true);
                _currentCourier = 5;
                break;
        }
    }
}
