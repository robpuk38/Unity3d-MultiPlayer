using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterreactHoverCraftManager : MonoBehaviour {

    public GameObject MePlayer;

    private void OnTriggerEnter(Collider other)
    {
        if(MePlayer.activeSelf == true)
        {
            MePlayer.SetActive(false);
        }
    }
}
