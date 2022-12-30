using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceView : MonoBehaviour
{
    [SerializeField]
    private PieceType _type;

    public PieceType Type => _type;

    public Vector3 WorldPosition => transform.position;

    public string Name => gameObject.name;

    internal void MoveTo(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }

    internal void Taken()
    {
        gameObject.SetActive(false);
    }

    internal void Placed(Vector3 worldPosition)
    {
        transform.position = worldPosition;
        gameObject.SetActive(true);
    }
}
