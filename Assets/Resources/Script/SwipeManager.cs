﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Swipe { None, Up, Down, Left, Right };

public class SwipeManager : MonoBehaviour
{
    public float minSwipeLength = 200f;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public static Swipe swipeDirection;

    void Update()
    {
        DetectSwipe();
    }
    
    public void DetectSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButton(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            if (currentSwipe.magnitude < minSwipeLength)
            {
                swipeDirection = Swipe.None;
                return;
            }

            currentSwipe.Normalize();

            // Swipe up
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) { swipeDirection = Swipe.Up; }
            // Swipe down
            else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) { swipeDirection = Swipe.Down; }
            // Swipe left
            else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) { swipeDirection = Swipe.Left; }
            // Swipe right
            else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) { swipeDirection = Swipe.Right; }
        }
        else { swipeDirection = Swipe.None; }
    }
}