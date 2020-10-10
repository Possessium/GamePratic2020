using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Substance.Game;

public class GP_Camera : MonoBehaviour
{
    [SerializeField] CameraState currentCamstate = CameraState.Free;
    [SerializeField] LayerMask interactableLayer = 0;

    [SerializeField] GameObject bell = null;
    [SerializeField] GameObject note = null;
    [SerializeField] GameObject map = null;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (currentCamstate == CameraState.Free) Free();
    }

    public void ChangeToFree() => ChangeState(CameraState.Free);

    void ChangeState(CameraState _state)
    {
        note.SetActive(false);
        bell.SetActive(false);
        map.SetActive(false);
        if (_state == CameraState.Free) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
        currentCamstate = _state;
    }

    void Free()
    {
        float _y = transform.eulerAngles.y + Input.GetAxis("Mouse X");
        float _x = transform.eulerAngles.x - Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(Mathf.Clamp(_x, 10, 50), _y, 0);

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