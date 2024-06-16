using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using UnityEditor;
using UnityEngine;

public class OisuController_2 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(new Vector3(1.3f, 1.3f, 1), 0.5f).SetEase(Ease.OutExpo);
        GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetEase(Ease.InExpo);
        DOVirtual.DelayedCall(0.5f, () =>
        {
            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
