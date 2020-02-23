using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Developer: Antoshka

public class Settings : MonoBehaviour, IPointerClickHandler
{
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
}
