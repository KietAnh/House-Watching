using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeCameraUI : MonoBehaviour
{
    public void IncreaseCameraIndex()
    {
        Dropdown dropdown = gameObject.GetComponent<Dropdown>();
        List<Dropdown.OptionData> opts = dropdown.options;
        int nVals = opts.Count;
        int val = dropdown.value;

        val += 1;
        if (val >= nVals)
        {
            dropdown.value = 0;
        }
        else
        {
            dropdown.value = val;
        }
    }

    public void DecreaseCameraIndex()
    {
        Dropdown dropdown = gameObject.GetComponent<Dropdown>();
        List<Dropdown.OptionData> opts = dropdown.options;
        int nVals = opts.Count;
        int val = dropdown.value;

        val -= 1;
        if (val < 0)
        {
            dropdown.value = nVals - 1;
        }
        else
        {
            dropdown.value = val;
        }
    }
}
