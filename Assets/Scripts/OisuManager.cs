using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class OisuManager : MonoBehaviour
{
    [SerializeField] GameObject OisuObject;
    [SerializeField] GameObject BottomBar;
    public float OisuPos_Current = 180;
    public float OisuPos_Destination = 180;
    public int OisuCount = 0;
    public float OisuInterval = 70;
    public float OisuMoveTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        OisuPos_Current = OisuPos_Destination;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CallOisu(string Name)
    {
        OisuCount++;
        GameObject go = Instantiate(OisuObject);
        go.transform.SetParent(BottomBar.transform);
        go.transform.localPosition = new Vector3(0, 9999, 0);
        go.transform.localScale = new Vector3(1, 1, 1);
        OisuPos_Destination -= OisuInterval;
        DOTween.To(() => OisuPos_Current, (n) => OisuPos_Current = n, OisuPos_Destination, OisuMoveTime);
        go.transform.GetChild(0).GetComponent<Text>().text = Name + "さんが";
        go.GetComponent<OisuObjectController>().OisuNumber = OisuCount;
        go.GetComponent<OisuObjectController>().manager = GetComponent<OisuManager>();
    }
}
