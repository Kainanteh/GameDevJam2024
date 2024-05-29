using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class AudioGolpeBajo : MonoBehaviour
{
    private AudioSource audioSource;

    public float[] samples = new float[512];
    public float[] freqBand = new float[8];
    public float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];

    public float beatThreshold = 1.0f;  // Umbral para detectar un beat
    public float minTimeBetweenBeats = 0.5f;  // Tiempo mnimo entre beats en segundos

    public GameObject beatObject;  // Objeto que cambiar de color
    public Color beatColor = Color.red;  // Color al detectar un beat
    public Color normalColor = Color.white;  // Color normal
    private Renderer objectRenderer;

    private bool previousBeat = false;  // Estado anterior del beat
    private float lastBeatTime = 0.0f;  // Tiempo del ltimo beat detectado

    public static event EventHandler AudioGolpeBajoEvento;




    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (beatObject != null)
        {
            objectRenderer = beatObject.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                objectRenderer.material.color = normalColor;
            }
        }
 


    }

 

    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        DetectBeat();

    }

    void GetSpectrumAudioSource()
    {
        /* audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);*/

        if (audioSource.isPlaying)
        {


            audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
 


        }
    }

    void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBand[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBand[i];
                bufferDecrease[i] = 0.005f;
            }
            else if (freqBand[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }

    void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBand[i] = average * 10;
        }
    }

    void DetectBeat()
    {
        // Calcula la intensidad promedio de las bandas bajas (bandas 0 y 1)
        float bassIntensity = (freqBand[0] + freqBand[1]) / 2;


        // Si la intensidad supera el umbral y ha pasado suficiente tiempo desde el ltimo beat
        bool isBeat = bassIntensity > beatThreshold;

        if (isBeat && !previousBeat && Time.time - lastBeatTime > minTimeBetweenBeats)
        {
            lastBeatTime = Time.time;  // Actualiza el tiempo del ltimo beat detectado
            ChangeColor(beatColor);

            // Evento
            AudioGolpeBajoEvento?.Invoke(this, EventArgs.Empty);

        }
        else if (!isBeat && previousBeat)
        {
            ChangeColor(normalColor);

        }



        previousBeat = isBeat;  // Actualiza el estado del beat anterior
    }

    void ChangeColor(Color color)
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = color;
        }
    }

}
