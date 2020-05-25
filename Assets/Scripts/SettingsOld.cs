using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Developer: Antoshka

public class SettingsOld : MonoBehaviour, IPointerClickHandler
{
    public Toggle MusicToggle;
    public Image MusicToggleImage;
    public Sprite MusicOn;
    public Sprite MusicOff;

    public GameObject Template;
    public Image _currentArrowImageComponent;

    public Sprite arrowOpened;
    public Sprite arrowClosed;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.Equals(Template))
        {
            _currentArrowImageComponent.sprite = Template.gameObject.activeInHierarchy ? arrowClosed : arrowClosed;
        }
    }

    public void OnOffMusic()
    {
        if (MusicToggle.isOn)
            MusicToggleImage.sprite = MusicOn;
        else
            MusicToggleImage.sprite = MusicOff;
    }
}
