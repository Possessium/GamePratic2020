using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_GameManager : MonoBehaviour
{
    public static GP_GameManager I { get; private set; }
    public bool IsPlay { get; private set; } = false;

    [SerializeField] Locations correctLocation = Locations.MTP;
    [SerializeField] GameObject startUI = null;
    [SerializeField] GameObject endUI = null;

    [SerializeField] UnityEngine.UI.Button crossMTP = null;
    [SerializeField] UnityEngine.UI.Button crossTarbes = null;
    [SerializeField] UnityEngine.UI.Button crossAlbi = null;

    [SerializeField] List<GameObject> allBells = new List<GameObject>();

    public GameObject SelectedBell { get; private set; } = null;

    [SerializeField] Transform spawnBell = null;
    public int Bell { get; private set; } = 0;

    [SerializeField] GameObject cursor = null;

    [SerializeField] List<GameObject> fion = new List<GameObject>();

    bool won = false;

    private void Awake()
    {
        I = this;
    }

    private void Update()
    {
        if (!won) return;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (SelectedBell)
            {
                SelectedBell.transform.RotateAround(SelectedBell.transform.position, new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0), 2);
            }
        }
    }

    public void StartGame()
    {
        startUI.SetActive(false);
        int _key = PlayerPrefs.GetInt("BellsDone");
        if(_key == 3) Bell = Random.Range(0, 3);
        else Bell = PlayerPrefs.GetInt("BellsDone");
        SelectedBell = Instantiate(allBells[Bell], spawnBell.position, Quaternion.identity);
        correctLocation = (Locations)Bell;
        Cursor.lockState = CursorLockMode.Locked;
        IsPlay = true;
    }

    public void QuitGame() => Application.Quit();

    public void ChooseMap(string _l)
    {
        if (SelectedBell.GetComponent<GP_BellGuider>().Bell.GetComponent<GP_Bell>().Stepsdone != 9) return;
        if (_l == "MTP")
        {
            if (correctLocation == Locations.MTP) Win();
            else
            {
                crossMTP.enabled = false;
                crossMTP.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        if (_l == "TARBES")
        {
            if (correctLocation == Locations.TARBES) Win();
            {
                crossTarbes.enabled = false;
                crossTarbes.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        if (_l == "ALBI")
        {
            if (correctLocation == Locations.ALBI) Win();
            {
                crossAlbi.enabled = false;
                crossAlbi.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    void Win()
    {
        fion[(int)correctLocation].SetActive(true);
        GP_SoundManager.I.Playdialogue(correctLocation == Locations.ALBI ? SoundsDialogue.doneAlbi : correctLocation == Locations.MTP ? SoundsDialogue.doneMTP : SoundsDialogue.doneLourdes);
        if(PlayerPrefs.GetInt("BellsDone") < 3) PlayerPrefs.SetInt("BellsDone", Bell+1);
        endUI.SetActive(true);
        won = true;
        IsPlay = false;
        GP_Camera.I.ChangeState(CameraState.Free);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Camera.main.transform.eulerAngles = new Vector3(18, 13, 0); 
        cursor.SetActive(false);
    }

    public void Restart() => UnityEngine.SceneManagement.SceneManager.LoadScene(0);
}

public enum Locations
{
    MTP,
    TARBES,
    ALBI
}
