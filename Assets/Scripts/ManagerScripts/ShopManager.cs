using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    public static event Action<int> CourierChanged;

    [SerializeField] private Button _openShopButton;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private TextMeshProUGUI _redCourierPriceTag;
    [SerializeField] private TextMeshProUGUI _greenCourierPriceTag;
    [SerializeField] private TextMeshProUGUI _pinkCourierPriceTag;
    [SerializeField] private TextMeshProUGUI _greyCourierPriceTag;
    [SerializeField] private TextMeshProUGUI _yellowCourierPriceTag;
    [SerializeField] private Image _greenFrame;

    private Vector3 _defaultCourierButtonPos = new(-200, 0, 0);
    private Vector3 _redCourierButtonPos = new(0, 0, 0);
    private Vector3 _greenCourierButtonPos = new(200, 0, 0);
    private Vector3 _pinkCourierButtonPos = new(-200, -230, 0);
    private Vector3 _greyCourierButtonPos = new(0, -230, 0);
    private Vector3 _yellowCourierButtonPos = new(200, -230, 0);

    private int _redCourierPrice = 1400;
    private int _greenCourierPrice = 1500;
    private int _pinkCourierPrice = 1600;
    private int _greyCourierPrice = 1700;
    private int _yellowCourierPrice = 1800;


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

    private void Start() 
    {
        ShowCourierPrices();
        PlaceGreenFrame();
    }


    public void OpenShopPanel()
    {
        _shopPanel.SetActive(true);
    }

    public void CloseShopPanel()
    {
        _shopPanel.SetActive(false);
    }

    public void ActivateShopButton()
    {
        _openShopButton.gameObject.SetActive(true);
    }

    public void DeactivateShopButton()
    {
        _openShopButton.gameObject.SetActive(false);
    }

    public void UseDefaultCourier()
    {
        if (PlayerPrefs.GetInt("ActiveCourier") != 0)
        {
            CourierChanged?.Invoke(0);
            PlayerPrefs.SetInt("ActiveCourier", 0);
            _greenFrame.rectTransform.localPosition = _defaultCourierButtonPos;
        }
    }

    public void BuyAndUseRedCourier()
    {
        if (PlayerPrefs.HasKey("RedCourierBought"))
        {
            if (PlayerPrefs.GetInt("ActiveCourier") != 1)
            {
                CourierChanged?.Invoke(1);
                PlayerPrefs.SetInt("ActiveCourier", 1);
                _greenFrame.rectTransform.localPosition = _redCourierButtonPos;
            }
        }
        else
        {
            float totalScore = PlayerPrefs.GetFloat("TotalScore");
            if (totalScore >= _redCourierPrice)
            {
                CourierChanged?.Invoke(1);
                PlayerPrefs.SetFloat("TotalScore", totalScore - _redCourierPrice);
                PlayerPrefs.SetInt("RedCourierBought", 1);
                PlayerPrefs.SetInt("ActiveCourier", 1);
                ScoreManager.Instance.DecreaseTotalScore(_redCourierPrice);
                _redCourierPriceTag.text = "USE";
                _greenFrame.rectTransform.localPosition = _redCourierButtonPos;
            }
        }
    }

    public void BuyAndUseGreenCourier()
    {
        if (PlayerPrefs.HasKey("GreenCourierBought"))
        {
            if (PlayerPrefs.GetInt("ActiveCourier") != 2)
            {
                CourierChanged?.Invoke(2);
                PlayerPrefs.SetInt("ActiveCourier", 2);
                _greenFrame.rectTransform.localPosition = _greenCourierButtonPos;
            }
        }
        else
        {
            float totalScore = PlayerPrefs.GetFloat("TotalScore");
            if (totalScore >= _greenCourierPrice)
            {
                CourierChanged?.Invoke(2);
                PlayerPrefs.SetFloat("TotalScore", totalScore - _greenCourierPrice);
                PlayerPrefs.SetInt("GreenCourierBought", 1);
                PlayerPrefs.SetInt("ActiveCourier", 2);
                ScoreManager.Instance.DecreaseTotalScore(_greenCourierPrice);
                _greenCourierPriceTag.text = "USE";
                _greenFrame.rectTransform.localPosition = _greenCourierButtonPos;
            }
        }
    }

    public void BuyAndUsePinkCourier()
    {
        
        if (PlayerPrefs.HasKey("PinkCourierBought"))
        {
            if (PlayerPrefs.GetInt("ActiveCourier") != 3)
            {
                CourierChanged?.Invoke(3);
                PlayerPrefs.SetInt("ActiveCourier", 3);
                _greenFrame.rectTransform.localPosition = _pinkCourierButtonPos;
            }
        }
        else
        {
            float totalScore = PlayerPrefs.GetFloat("TotalScore");
            if (totalScore >= _pinkCourierPrice)
            {
                CourierChanged?.Invoke(3);
                PlayerPrefs.SetFloat("TotalScore", totalScore - _pinkCourierPrice);
                PlayerPrefs.SetInt("PinkCourierBought", 1);
                PlayerPrefs.SetInt("ActiveCourier", 3);
                ScoreManager.Instance.DecreaseTotalScore(_pinkCourierPrice);
                _pinkCourierPriceTag.text = "USE";
                _greenFrame.rectTransform.localPosition = _pinkCourierButtonPos;
            }
        }
    }

    public void BuyAndUseGreyCourier()
    {
        
        if (PlayerPrefs.HasKey("GreyCourierBought"))
        {
            if (PlayerPrefs.GetInt("ActiveCourier") != 4)
            {
                CourierChanged?.Invoke(4);
                PlayerPrefs.SetInt("ActiveCourier", 4);
                _greenFrame.rectTransform.localPosition = _greyCourierButtonPos;
            }
        }
        else
        {
            float totalScore = PlayerPrefs.GetFloat("TotalScore");
            if (totalScore >= _greyCourierPrice)
            {
                CourierChanged?.Invoke(4);
                PlayerPrefs.SetFloat("TotalScore", totalScore - _greyCourierPrice);
                PlayerPrefs.SetInt("GreyCourierBought", 1);
                PlayerPrefs.SetInt("ActiveCourier", 4);
                ScoreManager.Instance.DecreaseTotalScore(_greyCourierPrice);
                _greyCourierPriceTag.text = "USE";
                _greenFrame.rectTransform.localPosition = _greyCourierButtonPos;
            }
        }
    }

    public void BuyAndUseYellowCourier()
    {
        
        if (PlayerPrefs.HasKey("YellowCourierBought"))
        {
            if (PlayerPrefs.GetInt("ActiveCourier") != 5)
            {
                CourierChanged?.Invoke(5);
                PlayerPrefs.SetInt("ActiveCourier", 5);
                _greenFrame.rectTransform.localPosition = _yellowCourierButtonPos;
            }
        }
        else
        {
            float totalScore = PlayerPrefs.GetFloat("TotalScore");
            if (totalScore >= _yellowCourierPrice)
            {
                CourierChanged?.Invoke(5);
                PlayerPrefs.SetFloat("TotalScore", totalScore - _yellowCourierPrice);
                PlayerPrefs.SetInt("YellowCourierBought", 1);
                PlayerPrefs.SetInt("ActiveCourier", 5);
                ScoreManager.Instance.DecreaseTotalScore(_yellowCourierPrice);
                _yellowCourierPriceTag.text = "USE";
                _greenFrame.rectTransform.localPosition = _yellowCourierButtonPos;
            }
        }
    }


    private void ShowCourierPrices()
    {
        if (!PlayerPrefs.HasKey("RedCourierBought"))
        {
            _redCourierPriceTag.text = _redCourierPrice.ToString();
        }

        if (!PlayerPrefs.HasKey("GreenCourierBought"))
        {
            _greenCourierPriceTag.text = _greenCourierPrice.ToString();
        }

        if (!PlayerPrefs.HasKey("PinkCourierBought"))
        {
            _pinkCourierPriceTag.text = _pinkCourierPrice.ToString();
        }

        if (!PlayerPrefs.HasKey("GreyCourierBought"))
        {
            _greyCourierPriceTag.text = _greyCourierPrice.ToString();
        }

        if (!PlayerPrefs.HasKey("YellowCourierBought"))
        {
            _yellowCourierPriceTag.text = _yellowCourierPrice.ToString();
        }
    }

    private void PlaceGreenFrame()
    {
        switch (PlayerPrefs.GetInt("ActiveCourier"))
        {
            case 0:
                _greenFrame.rectTransform.localPosition = _defaultCourierButtonPos;
                break;
            case 1:
                _greenFrame.rectTransform.localPosition = _redCourierButtonPos;
                break;
            case 2:
                _greenFrame.rectTransform.localPosition = _greenCourierButtonPos;
                break;
            case 3:
                _greenFrame.rectTransform.localPosition = _pinkCourierButtonPos;
                break;
            case 4:
                _greenFrame.rectTransform.localPosition = _greyCourierButtonPos;
                break;
            case 5:
                _greenFrame.rectTransform.localPosition = _yellowCourierButtonPos;
                break;
        }
    }
}
