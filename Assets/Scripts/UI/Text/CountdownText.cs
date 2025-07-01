using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownText : MonoBehaviour
{
    private TMP_Text countdownText;

    public float delayBetweenNumbers = 1f;

    private void Awake()
    {
        countdownText = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i >= 1; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(delayBetweenNumbers);
        }

        countdownText.gameObject.SetActive(false);
       
    }
}
