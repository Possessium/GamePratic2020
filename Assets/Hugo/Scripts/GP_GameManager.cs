using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_GameManager : MonoBehaviour
{
    public static GP_GameManager I { get; private set; }
    public bool IsPlay { get; private set; } = false;

    [SerializeField] Locations correctLocation = Locations.a;

    public int Bell { get; private set; } = 0;


    private void Awake()
    {
        IsPlay = true;
        Cursor.lockState = CursorLockMode.Locked;
        I = this;
    }

    private void Start()
    {
        Bell = Random.Range(0, 3);
    }

    public void ChooseMap(string _l)
    {
        if (_l == "MTP")
        {
            if (correctLocation == Locations.a) Win();
            else Loose();
        }
        if (_l == "DTC")
        {
            if (correctLocation == Locations.b) Win();
            else Loose();
        }
        if (_l == "IDK")
        {
            if (correctLocation == Locations.c) Win();
            else Loose();
        }
    }

    void Win()
    {

    }

    void Loose()
    {

    }
}

public enum Locations
{
    a,
    b,
    c
}
