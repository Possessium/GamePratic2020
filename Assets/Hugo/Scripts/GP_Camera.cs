using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Substance.Game;

public class GP_Camera : MonoBehaviour
{
    public static GP_Camera I { get; private set; }

    [SerializeField] CameraState currentCamstate = CameraState.Free;
    [SerializeField] LayerMask interactableLayer = 0;

    [SerializeField] GameObject bell = null;
    [SerializeField] GameObject note = null;
    [SerializeField] GameObject map = null;

    [SerializeField] GameObject cursor = null;


    private void Awake()
    {
        I = this;
    }

    private void Update()
    {
        if (!bell && GP_GameManager.I.SelectedBell)
        {
            bell = GP_GameManager.I.SelectedBell.GetComponent<GP_BellGuider>().Bell;
        }
        if (!GP_GameManager.I.IsPlay) return;
        if (currentCamstate == CameraState.Free) Free();
    }

    public void ChangeToFree()
    {
        Debug.Log(bell.name);
        bell.transform.parent.transform.eulerAngles = Vector3.zero;
        ChangeState(CameraState.Free);
    }

    public void ChangeState(CameraState _state)
    {
        if (!bell || !note || !map) return;
        note.SetActive(false);
        bell.SetActive(false);
        map.SetActive(false);
        transform.eulerAngles = new Vector3(15, 0, 0);
        if (_state == CameraState.Free)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cursor.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursor.SetActive(false);
        }
        currentCamstate = _state;
    }

    void Free()
    {
        Cursor.visible = false;
        cursor.SetActive(true);
        if (!bell || !note || !map) return;
        float _y = transform.eulerAngles.y + Input.GetAxis("Mouse X");
        float _x = transform.eulerAngles.x - Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(Mathf.Clamp(_x, 0, 80), _y, 0);

        RaycastHit _hit;
        if(Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, Mathf.Infinity, interactableLayer))
            {
                switch (_hit.transform.tag)
                {
                    case "Note":
                    ChangeState(CameraState.Note);
                    note.SetActive(true);
                        break;
                    case "Bell":
                    ChangeState(CameraState.Bell);
                    transform.eulerAngles = new Vector3(15, 0, 0);
                    bell.SetActive(true);
                        break;
                    case "Map":
                    ChangeState(CameraState.Map);
                    map.SetActive(true);
                        break;
                }
            }
    }
}

public enum CameraState
{
    Free,
    Bell,
    Note,
    Map
}