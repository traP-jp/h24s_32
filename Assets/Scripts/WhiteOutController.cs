using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class WhiteOutController : MonoBehaviour
{
    bool Pushed = false;
    public Image whiteOut;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Pushed)
        {
            DOVirtual.DelayedCall(0.5f, () =>
            {
                SceneManager.LoadScene("GameScene");
            });
            whiteOut.DOFade(1, 0.5f);
            Pushed = true;
        }
    }
}
