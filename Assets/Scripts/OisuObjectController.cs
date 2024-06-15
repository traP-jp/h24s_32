using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OisuObjectController : MonoBehaviour
{
    public float Interval = 0.3f;
    public OisuManager manager;
    public int OisuNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<Text>().DOFade(1, Interval);
        transform.GetChild(1).GetComponent<Text>().DOFade(1, Interval);
        transform.GetChild(2).GetComponent<Image>().DOFade(1, Interval);
        transform.GetChild(3).GetComponent<Image>().DOFade(1, Interval);
        transform.GetChild(4).GetComponent<Image>().DOFade(1, Interval);
        transform.GetChild(5).GetComponent<Image>().DOFade(1, Interval);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, manager.OisuPos_Current + (OisuNumber * manager.OisuInterval), 0);
        if (OisuNumber + 10 < manager.OisuCount)
        {
            Destroy(gameObject);
        }
    }
}
