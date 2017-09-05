using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    private bool move = false;
    private bool combine = false;
    public float speed = 0.2f;
    public int value;
    Vector2 curTarget;
    GameObject combineTarget;
    public static Sprite[] curImgArray;
    
    private void Start()
    {
        value = System.Convert.ToInt32(transform.FindChild("Text").GetComponent<TextMesh>().text);
        combineTarget = null;
    }

    public bool isMoving()
    {
        return move;
    }

    public bool Combine
    {
        get
        {
            return combine;
        }

        set
        {
            combine = value;
        }
    }

    private void Update()
    {
        if(move)
        {
            if(combineTarget != null)
            {
                Move(combineTarget);
            }
            else
            {
                Move(curTarget);
            }
        }
    }

    public static int getPow(int value)
    {
        int count = 0;

        while(true)
        {
            value /= 2;
            count++;

            if(value == 1) { break; }
        }

        return count;
    }

    public void Move(Vector2 target)
    {
        move = true;
        curTarget = target;
        transform.position = Vector2.MoveTowards(transform.position, target, speed);

        if (curTarget == (Vector2)transform.position)
        {
            move = false;
            GameObject.Find("SquareManager").GetComponent<SquareManager>().CreateSquare();
        }
    }

    public void Move(GameObject target)
    {
        
        move = true;
        combineTarget = target;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed);

        if (target.transform.position == transform.position)
        {
            Debug.Log("합체!");
            ScoreManager.setScore();
            move = false;
            //target.transform.FindChild("Text").GetComponent<TextMesh>().text = target.GetComponent<Square>().value.ToString();
            if(getPow(target.GetComponent<Square>().value) - 1 < curImgArray.Length)            
                target.GetComponent<SpriteRenderer>().sprite = curImgArray[getPow(target.GetComponent<Square>().value) - 1];            
            
            GameObject.Find("SquareManager").GetComponent<SquareManager>().CreateSquare();
            Destroy(gameObject);
        }
    }

    public void SetSprite(Sprite[] imgArray)
    {
        curImgArray = imgArray;
        string numberText = transform.FindChild("Text").GetComponent<TextMesh>().text;
        int number = System.Convert.ToInt32(numberText);

        GetComponent<SpriteRenderer>().sprite = imgArray[(number / 2) - 1];
    }
}
