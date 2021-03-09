using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu Instance;

    public bool jumping = false;
    public bool spinning = false;
    public bool pingPonging = false;
    public bool playingAnimation = false;
    
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }
    
    void Update()
    {
        switch (GameObject.FindObjectOfType<Square>().ActionFinished())
        {
            case "jumping":
                jumping = false;
                break;
            case "spinning":
                spinning = false;
                break;
            case "pingPonging":
                pingPonging = false;
                break;
            case "playingAnimation":
                playingAnimation = false;
                break;
        }
    }

    private void OnEnable()
    {
    }

    public void Jump()
    {
        jumping = true;
    }
    
    public void Spin()
    {
        spinning = true;
    }

    public void PingPong()
    {
        pingPonging = true;
    }

    public void PlayAnimation()
    {
        playingAnimation = true;
    }
}
