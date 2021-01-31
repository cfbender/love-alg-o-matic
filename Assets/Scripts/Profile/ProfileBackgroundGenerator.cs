using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileBackgroundGenerator : MonoBehaviour
{
    public Color32[] colors;
    public Image shadowImage;


    Color32 GetRandomColor()
    {
        return colors[Random.Range(0, colors.Length)];
    }

    void Start()
    {
        shadowImage.color = GetRandomColor();
    }

}
