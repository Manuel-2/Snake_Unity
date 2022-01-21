using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class PostProcesingManager : MonoBehaviour
{
    public static PostProcesingManager sharedInstance;

    [SerializeField] PostProcessVolume volume;

    private void Awake()
    {
        if(sharedInstance == null)
            sharedInstance = this;
        else
            Destroy(this.gameObject);

    }

    IEnumerator ColorLoop()
    {
        int index = -180;
        while (true)
        {
            ChangeHueShiftInProfile(index);
            index++;
            if(index > 180)
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
        }
    }
}
