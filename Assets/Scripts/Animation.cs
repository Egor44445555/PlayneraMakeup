using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Animation
{
    public ItemType type;
    public float moveSpeed;
    public int endBasedSpeedPosition = 0;
    public int startPositionAnimation = 0;
    public int startSecondPositionAnimation = 0;
    public List<RectTransform> path = new List<RectTransform>();    

    public Animation(List<RectTransform> _path, float _moveSpeed, ItemType _type, int _endBasedSpeedPosition, int _startPositionAnimation, int _startSecondPositionAnimation)
    {
        path = _path;
        moveSpeed = _moveSpeed;
        endBasedSpeedPosition = _endBasedSpeedPosition;
        startPositionAnimation = _startPositionAnimation;
        startSecondPositionAnimation = _startSecondPositionAnimation;
        type = _type;
    }
}
