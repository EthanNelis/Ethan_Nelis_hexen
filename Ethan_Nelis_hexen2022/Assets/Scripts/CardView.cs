using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private CardType _type;

    public CardType Type => _type;

    [SerializeField]
    private Image _image;

    public Image Image => _image;

    private GameObject _draggedCard;

    public GameObject DraggedCard => _draggedCard;


    public void OnBeginDrag(PointerEventData eventData)
    {
        _draggedCard = Instantiate(gameObject);

        _draggedCard.transform.SetParent(GameObject.Find("Canvas").transform);

        Image draggedCardImage = _draggedCard.GetComponent<Image>();

        draggedCardImage.preserveAspect = true;

        draggedCardImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _draggedCard.transform.position = Input.mousePosition;
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
