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
    [SerializeField] GameObject hammer = null;

    [SerializeField] string heldObject = "";
    [SerializeField] GameObject uiSelected = null;
    [SerializeField] LayerMask bellStepsLayer = 0;

    [SerializeField] List<GameObject> uis = new List<GameObject>();

    bool done = false;

    int stepsdone = 0;
    int fixdone = 0;
    int hammerdone = 0;
    int cleandone = 0;

    void Update()
    {
        if (!GP_GameManager.I.IsPlay) return;
        done = stepsdone == 9;
        if (Input.GetKey(KeyCode.Mouse1) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, rotateAreaLayer))
        {
            if (bell)
            {
                bell.transform.RotateAround(bell.transform.position, new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0), 2);
            }
        }
        if (done)
        {
            uis.ForEach(_ui => _ui.SetActive(false));
        }
        else
        {
            switch (currentState)
            {
                case BellState.Fix:
                    Fix();
                    break;
                case BellState.Clean:
                    Clean();
                    break;
                case BellState.Hammer:
                    Hammer();
                    break;
                default:
                    break;
            }
        }
    }

    public void ChangeStep(string _s)
    {
        if(_s == "Fix")
        {
            currentState = BellState.Fix;
            clean.SetActive(false);
            hammer.SetActive(false);
            fix.SetActive(true);
        }
        else if(_s == "Clean")
        {
            currentState = BellState.Clean;
            clean.SetActive(true);
            hammer.SetActive(false);
            fix.SetActive(false);
        }
        else if (_s == "Hammer")
        {
            heldObject = "Hammer";
            currentState = BellState.Hammer;
            clean.SetActive(false);
            hammer.SetActive(true);
            fix.SetActive(false);
        }
    }

    public void GrabObject(string _obj)
    {
        heldObject = _obj;
    }

    public void GrabUI(GameObject _go)
    {
        if (uiSelected) uiSelected.SetActive(false);
        _go.SetActive(true);
        uiSelected = _go;
    }

    void Fix()
    {
        if (!string.IsNullOrEmpty(heldObject))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit _hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, Mathf.Infinity, bellStepsLayer))
                {
                    if (_hit.transform.tag == heldObject)
                    {
                        _hit.transform.GetComponent<MeshRenderer>().enabled = false;
                        _hit.transform.GetComponent<Collider>().enabled = false;
                        _hit.transform.GetChild(0).gameObject.SetActive(true);
                        if (uiSelected) uiSelected.SetActive(false);
                        heldObject = "";
                        stepsdone++;
                        fixdone++;
                        if (fixdone == 3) GP_Note.I.AddPage(PageType.Info);
                    }
                    else
                    {
                        if (uiSelected) uiSelected.SetActive(false);
                        heldObject = "";
                    }
                }
                else
                {
                    if (uiSelected) uiSelected.SetActive(false);
                    heldObject = "";
                }
            }
        }
    }


    void Clean()
    {
        if (!string.IsNullOrEmpty(heldObject))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit _hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, Mathf.Infinity, bellStepsLayer))
                {
                    if (_hit.transform.tag == heldObject)
                    {
                        if(heldObject == "Dirty")
                        {
                            _hit.transform.tag = "Clean";
                        }
                        else
                        {
                            _hit.transform.GetComponent<MeshRenderer>().enabled = false;
                            _hit.transform.GetComponent<Collider>().enabled = false;
                            _hit.transform.GetChild(0).gameObject.SetActive(true);
                            stepsdone++;
                            cleandone++;
                            if (cleandone == 3) GP_Note.I.AddPage(PageType.Blason);
                        }
                        if (uiSelected) uiSelected.SetActive(false);
                        heldObject = "";
                    }
                    else
                    {
                        if (uiSelected) uiSelected.SetActive(false);
                        heldObject = "";
                    }
                }
                else
                {
                    if (uiSelected) uiSelected.SetActive(false);
                    heldObject = "";
                }
            }
        }
    }

    void Hammer()
    {
        if (!string.IsNullOrEmpty(heldObject))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit _hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, Mathf.Infinity, bellStepsLayer))
                {
                    if (_hit.transform.tag == heldObject)
                    {
                        _hit.transform.GetComponent<Collider>().enabled = false;
                        _hit.transform.GetChild(0).gameObject.SetActive(true);
                        if (uiSelected) uiSelected.SetActive(false);
                        stepsdone++;
                        hammerdone++;
                        if (hammerdone == 3) GP_Note.I.AddPage(PageType.Son);
                    }
                }
            }
        }
    }


}

enum BellState
{
    Fix,
    Clean,
    Hammer
}
