using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElement : MonoBehaviour
{
    [SerializeField]
    public float _radius;
    [SerializeField]
    public float _angle;
    [SerializeField]
    public float _posX;
    [SerializeField]
    public float _posY;
    [SerializeField]
    public float _deltaX;
    [SerializeField]
    public float _deltaY;
    [SerializeField]
    public bool _health = true;
}
