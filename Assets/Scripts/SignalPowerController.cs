using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SignalPowerController : MonoBehaviour
{
    [SerializeField] private GameObject pointToDestroy;
    [SerializeField] private GameObject[] signalSticks;
    [SerializeField] private float distanceToPoint;
    [SerializeField] private GameObject cameraPoint;
    [SerializeField] private Text distanceText;
    [SerializeField] private GameObject lowSignalTextObj;
    [SerializeField] private Volume globalVolume;
    private ColorAdjustments colorAdjustments;
    private FilmGrain filmGrain;
    private float firstMaxDistance = 250f;
    private float secondMaxDistance = 260f;
    private float thirdMaxDistance = 270f;
    private float fourthMaxDistance = 280f;

    private bool coroutineIsActive = false;
    private Coroutine activeCoroutine = null;

    private void Start()
    {
        colorAdjustments = GetColorAdjustment();
        filmGrain = GetFilmGrain();       
    }

    private void Update()
    {
        CalculateDistance();

        ChangeDistanceText();

        SwitchSignalSticks();

        SwitchObjWithPause();
        
        UpdateEffects();
    }

    private void UpdateEffects()
    {
        if (distanceToPoint >= firstMaxDistance && distanceToPoint <= fourthMaxDistance)
        {
            float t = Mathf.InverseLerp(firstMaxDistance, fourthMaxDistance, distanceToPoint);

            colorAdjustments.contrast.value = Mathf.Lerp(22f, -100f, t);
           
            filmGrain.intensity.value = Mathf.Lerp(0.06f, 0.95f, t);
        }
        else if (distanceToPoint < firstMaxDistance)
        {
            colorAdjustments.contrast.value = 22f;
            filmGrain.intensity.value = 0.06f;
        }
    }

    private FilmGrain GetFilmGrain()
    {
        if (globalVolume.profile.TryGet(out FilmGrain grain))
        {
            return filmGrain = grain;
        }
        return null;
    }

    private ColorAdjustments GetColorAdjustment()
    {
        if (globalVolume.profile.TryGet(out ColorAdjustments colorAdj))
        {
            return colorAdj;
        }
        return null;
    }

    private void SwitchObjWithPause()
    {
        if(!coroutineIsActive && distanceToPoint >= secondMaxDistance)
        {
            activeCoroutine = StartCoroutine(ToggleObject(lowSignalTextObj));
            coroutineIsActive = true;
        }
        else if (coroutineIsActive && distanceToPoint < secondMaxDistance)
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
        if (distanceToPoint >= firstMaxDistance && distanceToPoint < secondMaxDistance)
        {
            signalSticks[3].SetActive(false);
            signalSticks[2].SetActive(true);
        }
        else if (distanceToPoint >= secondMaxDistance && distanceToPoint < thirdMaxDistance)
        {
            signalSticks[2].SetActive(false);
            signalSticks[1].SetActive(true);
        }
        else if (distanceToPoint >= thirdMaxDistance)
        {
            signalSticks[1].SetActive(false);
        }
        else if (distanceToPoint >= fourthMaxDistance)
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
