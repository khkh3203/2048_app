using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

    public Sprite[] spriteArr = null;

    private void Awake()
    {

    }

    public Sprite GetSprite(string name)
    {
        Debug.Log(name);

        for (int i = 0; i < spriteArr.Length; ++i)
        {
            if (spriteArr[i].name.Equals(name))
            {
                return spriteArr[i];
            }
        }

        return null;
    }
}
