using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SignalPowerController : MonoBehaviour
{
    [SerializeField] private GameObject pointToDestroy;
    [SerializeField] private GameObject[] signalSticks;
    [SerializeField] private float distanceToPoint;
    [SerializeField] private GameObject cameraPoint;
    [SerializeField] private Text distanceText;
    [SerializeField] private GameObject lowSignalTextObj;

    private bool coroutineIsActive = false;
    private Coroutine activeCoroutine = null;


    private void Update()
    {
        CalculateDistance();

        ChangeDistanceText();

        SwitchSignalSticks();

        SwitchObjWithPause();
    }

    private void SwitchObjWithPause()
    {
        if(!coroutineIsActive && distanceToPoint >= 270)
        {
            activeCoroutine = StartCoroutine(ToggleObject(lowSignalTextObj));
            coroutineIsActive = true;
        }
        else if (coroutineIsActive && distanceToPoint < 270)
        {            
            StopCoroutine(activeCoroutine);
            coroutineIsActive = false;
            lowSignalTextObj.SetActive(false); 
        }
    }
    
    IEnumerator ToggleObject(GameObject obj)
    {
        while (true)
        {
            obj.SetActive(true);

            yield return new WaitForSeconds(0.5f);
           
            obj.SetActive(false);

            yield return new WaitForSeconds(0.5f);
        }
    }
    

    private void SwitchSignalSticks()
    {
        if (distanceToPoint >= 250f && distanceToPoint < 260f)
        {
            signalSticks[3].SetActive(false);
            signalSticks[2].SetActive(true);
        }
        else if (distanceToPoint >= 260f && distanceToPoint < 270f)
        {
            signalSticks[2].SetActive(false);
            signalSticks[1].SetActive(true);
        }
        else if (distanceToPoint >= 270f)
        {
            signalSticks[1].SetActive(false);
        }
        else if (distanceToPoint >= 280f)
        {
            signalSticks[0].SetActive(false);
        }
        else
        {
            signalSticks[3].SetActive(true);
        }
    }

    private void ChangeDistanceText()
    {
        distanceText.text = distanceToPoint.ToString("F2") + "m.";
    }

    private void CalculateDistance()
    {
        distanceToPoint = Vector3.Distance(cameraPoint.transform.position, pointToDestroy.transform.position);
    }
}
