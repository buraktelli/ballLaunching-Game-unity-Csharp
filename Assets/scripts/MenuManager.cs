﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void enable(int x)
    {
        PlayerPrefs.SetInt("sahne", x);
    }
    public void Load(int index)
    {
        SceneManager.LoadScene(index); 
    }
}
