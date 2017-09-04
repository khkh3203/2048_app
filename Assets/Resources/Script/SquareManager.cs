using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareManager : MonoBehaviour
{
    Color[] colorArray;
    public GameObject defaultSquare;
    bool created = false;
    bool dragging;
    bool full;
    Vector3 zeroIndexPosition;
    GameObject[,] squareArray;
    int topY = 0;
    int width = 4;
    int height = 4;

    private void Awake()
    {
        colorArray = new Color[10];
        for (int i = 0; i < 10; i++)
        {
            colorArray[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        }

        dragging = false;
        full = false;
        zeroIndexPosition = defaultSquare.transform.position;
        squareArray = new GameObject[width, height];
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(720, 1280, true);

    }

    private void Start()
    {
        CreateSquare();        
    }

    private void Update()
    {
        CheckArrayFull();
        bool check = false;
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            created = false;
            CreateSquare();
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    if(squareArray[y, x] == null) { continue; }
                    squareArray[y, x].GetComponent<Square>().Combine = false;
                }
            }
        }

        if (dragging) { return; }

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (squareArray[i, j] == null) { continue; }
                if (squareArray[i, j].GetComponent<Square>().isMoving()) { return; }
            }
        }
        
        switch (SwipeManager.swipeDirection)
        {
            case Swipe.Up:
                dragging = true;
                check = true;
                for (int y = 1; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (squareArray[y, x] == null) { continue; }

                        for (int i = y - 1; i >= 0; i--)
                        {
                            if (squareArray[i, x] != null && !squareArray[i, x].GetComponent<Square>().Combine)
                            {
                                int number = squareArray[i, x].GetComponent<Square>().value;
                                int number2 = squareArray[y, x].GetComponent<Square>().value;

                                if (number == number2)
                                {
                                    squareArray[i, x].GetComponent<Square>().value = number + number2;
                                    squareArray[i, x].GetComponent<Square>().Combine = true;
                                    squareArray[y, x].GetComponent<Square>().Move(squareArray[i, x]);
                                    squareArray[y, x] = null;
                                    full = false;

                                    break;
                                }

                                break;
                            }

                            if (squareArray[i, x] == null)
                            {
                                topY = i;
                            }
                        }
                        if (squareArray[topY, x] == null && squareArray[y, x] != null)
                        {
                            squareArray[topY, x] = squareArray[y, x];
                            squareArray[y, x] = null;
                            squareArray[topY, x].GetComponent<Square>().Move(new Vector2(zeroIndexPosition.x + (0.6f * x - topY + (x * 0.4f)) * 1.25f - 10, zeroIndexPosition.y - (0.6f * topY + x + (topY * 0.4f)) * 1.1f));
                            full = false;
                        }
                    }
                }
                if (check) { created = false; }
                break;
            case Swipe.Down:
                dragging = true;
                check = true;
                //4*4 = y=2
                //5*5 = y=3
                for (int y = 2; y >= 0; y--)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (squareArray[y, x] == null) { continue; }

                        for (int i = y + 1; i < height; i++)
                        {
                            if (squareArray[i, x] != null && !squareArray[i, x].GetComponent<Square>().Combine)
                            {
                                int number = squareArray[i, x].GetComponent<Square>().value;
                                int number2 = squareArray[y, x].GetComponent<Square>().value;

                                if (number == number2)
                                {
                                    squareArray[i, x].GetComponent<Square>().value = number + number2;
                                    squareArray[i, x].GetComponent<Square>().Combine = true;
                                    squareArray[y, x].GetComponent<Square>().Move(squareArray[i, x]);
                                    squareArray[y, x] = null;
                                    full = false;
                                    break;
                                }

                                break;
                            }

                            if (squareArray[i, x] == null)
                            {
                                topY = i;
                            }
                        }
                        if (squareArray[topY, x] == null && squareArray[y, x] != null)
                        {
                            squareArray[topY, x] = squareArray[y, x];
                            squareArray[y, x] = null;
                            squareArray[topY, x].GetComponent<Square>().Move(new Vector2(zeroIndexPosition.x + (0.6f * x - topY + (x * 0.4f)) * 1.25f - 10, zeroIndexPosition.y - (0.6f * topY + x + (topY *0.4f)) * 1.1f));                            
                            full = false;
                        }
                    }
                }
                if (check) { created = false; }
                break;
            case Swipe.Left:
                dragging = true;
                check = true;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 1; x < width; x++)
                    {
                        if (squareArray[y, x] == null) { continue; }

                        for (int i = x - 1; i >= 0; i--)
                        {
                            if (squareArray[y, i] != null && !squareArray[y, i].GetComponent<Square>().Combine)
                            {
                                int number = squareArray[y, i].GetComponent<Square>().value;
                                int number2 = squareArray[y, x].GetComponent<Square>().value;

                                if (number == number2)
                                {
                                    squareArray[y, i].GetComponent<Square>().value = number + number2;
                                    squareArray[y, i].GetComponent<Square>().Combine = true;
                                    squareArray[y, x].GetComponent<Square>().Move(squareArray[y, i]);
                                    squareArray[y, x] = null;
                                    full = false;
                                    break;
                                }

                                break;
                            }

                            if (squareArray[y, i] == null)
                            {
                                topY = i;
                            }
                        }
                        if (squareArray[y, topY] == null && squareArray[y, x] != null)
                        {
                            squareArray[y, topY] = squareArray[y, x];
                            squareArray[y, x] = null;
                            squareArray[y, topY].GetComponent<Square>().Move(new Vector2(zeroIndexPosition.x + (0.6f * topY - y + (topY * 0.4f)) * 1.25f - 10, zeroIndexPosition.y - (0.6f * y + topY + (y * 0.4f)) * 1.1f));
                            full = false;
                        }
                    }
                }
                if (check) { created = false; }
                break;
            case Swipe.Right:
                dragging = true;
                check = true;
                for (int y = 0; y < height; y++)
                {
                    //4*4  x= 2
                    //5*5  x= 3
                    for (int x = 2; x >= 0; x--)
                    {
                        if (squareArray[y, x] == null) { continue; }

                        for (int i = x + 1; i < width; i++)
                        {
                            if (squareArray[y, i] != null && !squareArray[y, i].GetComponent<Square>().Combine)
                            {
                                int number = squareArray[y, i].GetComponent<Square>().value;
                                int number2 = squareArray[y, x].GetComponent<Square>().value;

                                if (number == number2)
                                {
                                    squareArray[y, i].GetComponent<Square>().value = number + number2;
                                    squareArray[y, i].GetComponent<Square>().Combine = true;
                                    squareArray[y, x].GetComponent<Square>().Move(squareArray[y, i]);
                                    squareArray[y, x] = null;
                                    full = false;
                                    break;
                                }

                                break;
                            }

                            if (squareArray[y, i] == null)
                            {
                                topY = i;
                            }
                        }
                        if (squareArray[y, topY] == null && squareArray[y, x] != null)
                        {
                            squareArray[y, topY] = squareArray[y, x];
                            squareArray[y, x] = null;
                            squareArray[y, topY].GetComponent<Square>().Move(new Vector2(zeroIndexPosition.x + (0.6f * topY - y + (topY * 0.4f)) * 1.25f - 10, zeroIndexPosition.y - (0.6f * y + topY + (y * 0.4f)) * 1.1f));                            
                            full = false;
                        }
                    }
                }
                if (check) { created = false; }
                break;
            default:
                return;
        }
    }

    private void CheckArrayFull()
    {
        int check = 0;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (squareArray[i, j] == null) { continue; }
                check++;
            }
        }

        if (check >= width*height) { full = true; }
    }

    /// <summary>
    /// Creates a Square at the random index of the array
    /// </summary>
    public void CreateSquare()
    {
        if (full) { return; }
        if(created) { return; }
        created = true;

        int x = 0;
        int y = 0;
        while (true)
        {
            x = Random.Range(0, width);
            y = Random.Range(0, height);
            if (squareArray[y, x] == null) { break; }
        }

        squareArray[y, x] = Instantiate(defaultSquare, new Vector3(zeroIndexPosition.x + (0.6f * x-y+((x*0.4f)))*1.25f - 10, zeroIndexPosition.y - (0.6f * y+x+((y*0.4f)))*1.1f, zeroIndexPosition.z), Quaternion.identity);
//        squareArray[y, x] = Instantiate(defaultSquare, new Vector3(zeroIndexPosition.x + (1.2f * x), zeroIndexPosition.y - (1.2f * y), zeroIndexPosition.z), Quaternion.identity);
        squareArray[y, x].transform.FindChild("Text").GetComponent<TextMesh>().text = (Random.Range(0, width) > 2) ? 4.ToString() : 2.ToString();
        squareArray[y, x].GetComponent<Square>().SetColor(colorArray);
        squareArray[y, x].transform.Rotate(new Vector3(45, 0, -45));

    }
}