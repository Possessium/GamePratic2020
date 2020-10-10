using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_GameManager : MonoBehaviour
{
    [SerializeField] Locations correctLocation = Locations.a;

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
