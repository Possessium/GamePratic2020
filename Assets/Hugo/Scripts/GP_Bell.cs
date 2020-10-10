using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Bell : MonoBehaviour
{
    [SerializeField] BellState currentState = BellState.Fix;

    [SerializeField] LayerMask rotateAreaLayer = 0;
    [SerializeField] GameObject bell = null;

    [SerializeField] GameObject fix = null;
    [SerializeField] GameObject clean = null;
    [SerializeField] GameObject polish = null;

    #region Fix
    [SerializeField] GameObject heldObject = null;

    [SerializeField] LayerMask pieceLayer = 0;
    #endregion

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, rotateAreaLayer))
        {
            if(bell)
            {
                    bell.transform.RotateAround(bell.transform.position, new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0), 2);
            }
        }
        switch (currentState)
        {
            case BellState.Fix:
                Fix();
                break;
            case BellState.Clean:
                Clean();
                break;
            case BellState.Polish:
                Polish();
                break;
            default:
                break;
        }
    }

    void Fix()
    {
        /*
         * cast on Key sur layer piece
         *      follow piece de la souris
         * 
         * cast on KeyUp sur layer hole
         *      reset piece si pas bon tags
         *      fix si bon tags
         */
    }

    void Clean()
    {

    }

    void Polish()
    {

    }


}

enum BellState
{
    Fix,
    Clean,
    Polish
}