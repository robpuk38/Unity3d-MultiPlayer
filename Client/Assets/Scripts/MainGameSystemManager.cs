using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameSystemManager : MonoBehaviour {
    public GameObject MainCamera;
    public GameObject LoadedSystem;
    public GameObject NetworkManager;
    public GameObject ServerStatusSystem;
    private void Awake()
    {
        MainCamera.SetActive(true);
        ServerStatusSystem.SetActive(true);
        LoadedSystem.SetActive(false);
        NetworkManager.SetActive(false);
    }
}
