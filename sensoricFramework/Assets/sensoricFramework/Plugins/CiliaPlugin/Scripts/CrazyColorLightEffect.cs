﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyColorLightEffect : MonoBehaviour
{
    private uint NUMBEROFPIXELS = 6;

    [Header("The main player object or object which will trigger lights.")]
    [SerializeField] private GameObject MainCharacter;
    [Header("The Group of the Cilia you want to activate.")]
    [SerializeField] private SurroundPosition CiliaGroup = (SurroundPosition)0;
    [Header("How much time has to pass before the colors will change.")]
    [SerializeField] private float SpeedDividerMinOffset = 0.1f;
    [SerializeField] private float SpeedDividerMax = 2.0f;
    [Header("Base Color.")]
    [SerializeField] private byte Red = 0;
    [SerializeField] private byte Green = 0;
    [SerializeField] private byte Blue = 0;
    [Header("Should all the lights flicker together?")]
    [SerializeField] private bool FlickerAllTogether = false;

    private float mRandomTime = 0.0f;

    private float mTimer = 0.0f;

    /**
     * While the player is collided generate and play crazy color effect.
     * @param collision object that triggers effect.
     */
    void OnTriggerStay(Collider collision)
    {
        if (MainCharacter.Equals(collision.gameObject))
        {
            mTimer += Time.deltaTime;
            if (mTimer > mRandomTime)
            {
                mRandomTime = ((Random.Range(0, SpeedDividerMax)) + SpeedDividerMinOffset);
                mTimer = 0.0f;
                float random1Float = Random.Range(0.0f, 1.0f);
                float random2Float = Random.Range(0.0f, 1.0f);
                float random3Float = Random.Range(0.0f, 1.0f);
                for (byte LightNumber = 1; LightNumber < 7; LightNumber++)
                {
                    byte redSubtract = (byte)((float)Red * random1Float);
                    byte redValue = (byte)((int)Red - (int)redSubtract);
                    byte greenSubtract = (byte)((float)Green * random2Float);
                    byte greenValue = (byte)((int)Green - (int)greenSubtract);
                    byte blueSubtract = (byte)((float)Blue * random3Float);
                    byte blueValue = (byte)((int)Blue - (int)blueSubtract);
                    Cilia.setLight(CiliaGroup, LightNumber, redValue, greenValue, blueValue);
                    if (FlickerAllTogether == false)
                    {
                        random1Float = Random.Range(0.0f, 1.0f);
                        random2Float = Random.Range(0.0f, 1.0f);
                        random3Float = Random.Range(0.0f, 1.0f);
                    }
                }
            }
        }
    }
}
