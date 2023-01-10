using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private CardType _type;

    [SerializeField]
    private Image _image;

    private GameObject _draggedCard;


    public CardType Type => _type;

    public Image Image => _image;

    public GameObject DraggedCard => _draggedCard;


    public void OnDrag(PointerEventData eventData)
    {
        _draggedCard.transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _draggedCard = Instantiate(gameObject);

        _draggedCard.transform.SetParent(GameObject.Find("Canvas").transform);

        Image draggedCardImage = _draggedCard.GetComponent<Image>();

        draggedCardImage.preserveAspect = true;

        draggedCardImage.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(_draggedCard);
    }

    public void DestroyCard()
    {
        Destroy(_draggedCard);
        Destroy(gameObject);
    }
}
