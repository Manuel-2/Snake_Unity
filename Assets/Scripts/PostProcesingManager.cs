using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class PostProcesingManager : MonoBehaviour
{
    public static PostProcesingManager sharedInstance;

    [SerializeField] PostProcessVolume volume;
    [SerializeField] int[] lightColors;

    int currentShiftValue;
    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
        else
            Destroy(this.gameObject);

    }

    private void Start()
    {
        var arr = MenuManager.sharedInstance.colorShiftValues;
        int index = MenuManager.sharedInstance.defaultColorIndex;

        currentShiftValue = PlayerPrefs.GetInt(MenuManager.sharedInstance.playerGameColorConfigkey, index);
        CheckContrast(arr[currentShiftValue]);
    }

    IEnumerator ColorLoop()
    {
        int index = -180;
        while (true)
        {
            ChangeHueShiftInProfile(index);
            index++;
            if (index > 180)
            {
                index = -180;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void ChangeHueShiftInProfile(int v)
    {
        if (volume.profile.TryGetSettings(out ColorGrading colorGrading))
        {
            colorGrading.hueShift.value = v;
            CheckContrast(v);
        }
    }

    private void CheckContrast(int v)
    {
        for (int i = 0; i < lightColors.Length; i++)
        {
            if (v == lightColors[i])
            {

                MenuManager.sharedInstance.ChangeHeadersFont2Dark();
                return;
            }
        }
        MenuManager.sharedInstance.ChangeHeadersFont2Light();
    }
}
