using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsUpdater : MonoBehaviour
{
    public Text fpsText;

    private int count;
    private float deltaTime;


    private void Start()
    {
        fpsText = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        count++;
        deltaTime += Time.deltaTime;

        if (deltaTime >= 0.5f)
        {
            var fps = count / deltaTime;
            count = 0;
            deltaTime = 0;
            fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
        }
    }
}
