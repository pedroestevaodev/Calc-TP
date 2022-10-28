using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Themes : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Camera Camera;

    [SerializeField]
    private Text Display1;

    [SerializeField] 
    private Color whiteBackground;

    [SerializeField]
    private Color blackBackground;

    [SerializeField]
    private Color fontDisplay1White;

    [SerializeField]
    private Color fontDisplay1Black;

    [SerializeField]
    private Color whiteToggleBtn;

    [SerializeField]
    private Color blackToggleBtn;

    [SerializeField]
    private Boolean _isOn = false;
    private Boolean isOn
    {
        get
        {
            return _isOn;
        }
    }

    [SerializeField]
    private RectTransform ToggleBtn;

    [SerializeField]
    private Image ToggleBtnColor;

    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private Color toggleDark;

    [SerializeField]
    private Color toggleWhite;
    private float offX;
    private float onX;

    [SerializeField]
    private float tweenTime = 0.25f;

    public delegate void ValueChanged(Boolean value);
    public event ValueChanged valueChanged;

    private void Start()
    {
        offX = ToggleBtn.anchoredPosition.x;
        onX = backgroundImage.rectTransform.rect.width - ToggleBtn.rect.width;
    }

    private void OnEnable()
    {
        ToggleColor(isOn);
    }

    public void Toggle(Boolean value)
    {
        if(value != isOn)
        {
            _isOn = value;

            ToggleColor(isOn);
            MoveToggle(isOn);

            if(valueChanged != null)
            {
                valueChanged(isOn);
            }
        }
    }

    private void ToggleColor(Boolean value)
    {
        if(value)
        {
            backgroundImage.DOColor(toggleDark, tweenTime);
            ToggleBtnColor.DOColor(blackToggleBtn, tweenTime);
            Camera.DOColor(blackBackground, tweenTime);
            Display1.DOColor(fontDisplay1Black, tweenTime);
        }
        else
        {
            backgroundImage.DOColor(toggleWhite, tweenTime);
            ToggleBtnColor.DOColor(whiteToggleBtn, tweenTime);
            Camera.DOColor(whiteBackground, tweenTime);
            Display1.DOColor(fontDisplay1White, tweenTime);
        }
    }

    private void MoveToggle(Boolean value)
    {
        if(value)
        {
            ToggleBtn.DOAnchorPosX(onX, tweenTime);
        }
        else
        {
            ToggleBtn.DOAnchorPosX(offX, tweenTime);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Toggle(!isOn);
    }
}
