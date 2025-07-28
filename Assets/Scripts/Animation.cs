using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Animation
{
    public ItemType type;
    public float moveSpeed;
    public int startPositionAnimation = 0;
    public List<RectTransform> path = new List<RectTransform>();    

    public Animation(List<RectTransform> _path, float _moveSpeed, ItemType _type, int _startPositionAnimation)
    {
        path = _path;
        moveSpeed = _moveSpeed;
        startPositionAnimation = _startPositionAnimation;
        type = _type;
    }
}
