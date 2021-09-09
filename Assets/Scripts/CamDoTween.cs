using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamDoTween : MonoBehaviour
{
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private Transform camTranform;
    [SerializeField] private Transform tempTransform;
    [SerializeField] private Transform finishCamPos;
    private Vector3 targetVectorpos;
    private Vector3 finishVectorpos;
   
    

   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForGate());        
    }
    

    private IEnumerator WaitForGate()
    {
        yield return new WaitForSeconds(1);
        targetVectorpos = tempTransform.position;   
        camTranform.DOMove(targetVectorpos, 1);
        camTranform.DORotate(new Vector3(29, 0, 0), 1);
    }
    private IEnumerator WaitForEndGame()
    {
        finishVectorpos = finishCamPos.position;
        camTranform.DOMove(finishVectorpos, 1.5f);
        camTranform.DORotate(new Vector3(39, 270, 0), 1.5f);
        yield return new WaitForSeconds(1);
        winCanvas.SetActive(true);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CamFixer"))
        {
            StartCoroutine(WaitForEndGame());
        }
    }


}
