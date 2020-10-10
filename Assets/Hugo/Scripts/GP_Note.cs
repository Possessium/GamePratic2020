using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Note : MonoBehaviour
{
    public static GP_Note I { get; private set; }

    [SerializeField] List<GameObject> blasonPages = new List<GameObject>();
    [SerializeField] List<GameObject> sonPages = new List<GameObject>();
    [SerializeField] List<GameObject> infoPages = new List<GameObject>();

    [SerializeField] GameObject noteObject = null;

    int index = 0;


    private void Awake()
    {
        I = this;
    }

    public void TurnPage(bool _right)
    {
        if (!_right && index == 0) return;
        if (_right && index == noteObject.transform.childCount -1) return;
        index = _right ? index+1 : index-1;
        for (int i = 0; i < noteObject.transform.childCount; i++)
        {
            noteObject.transform.GetChild(i).gameObject.SetActive(i == index);
            noteObject.transform.GetChild(i).GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    public void AddPage(PageType _type)
    {
        switch (_type)
        {
            case PageType.Blason:
                blasonPages[GP_GameManager.I.Bell].transform.SetParent(noteObject.transform);
                break;
            case PageType.Son:
                sonPages[GP_GameManager.I.Bell].transform.SetParent(noteObject.transform);
                break;
            case PageType.Info:
                infoPages[GP_GameManager.I.Bell].transform.SetParent(noteObject.transform);
                break;
            default:
                break;
        }
    }

}

public enum PageType
{
    Blason,
    Son,
    Info
}
