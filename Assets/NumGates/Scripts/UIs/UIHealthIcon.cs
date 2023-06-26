using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthIcon : MonoBehaviour
{
    [SerializeField] private Image image;

    public void Init()
    {
        image.color = Color.red;
    }

    public void Damaged()
    {
        image.color = Color.gray;
    }
}
