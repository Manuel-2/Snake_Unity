using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitterLink : MonoBehaviour
{
    [SerializeField] string link;

    public void OpenLink()
    {
        Application.OpenURL(link);
    }
}
