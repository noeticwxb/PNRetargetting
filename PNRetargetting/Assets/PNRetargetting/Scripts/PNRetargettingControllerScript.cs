/***************************************************
 * Written By: Richard Borys
 * Feel free to modify and share, this is meant as a
 * starting point and tool to help other developers
 * implement Perception Neuron into their Unity 
 * Projects.
****************************************************/

using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

/// <summary>
/// Used so that PN can work with a character that runs around the level.
/// Requires enabling/disabling of retargetting to give animator control.
/// </summary>
public class PNRetargettingControllerScript : MonoBehaviour {

    public bool enablePositionMarker = false;
    public GameObject positionMarkerPrefab;

    private PNRetargetting retargettingPN;
    private bool controlledByPN = true;
    private GameObject positionMarker;

    void Start()
    {
        retargettingPN = GetComponent<PNRetargetting>();

        GetComponent<Animator>().enabled = false;
        GetComponent<ThirdPersonUserControl>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            controlledByPN = !controlledByPN;

            if(controlledByPN)
            {
                TogglePNControl();
            }
            else
            {
                ToggleAnimControl();
            }
        }

        if(enablePositionMarker)
        {
            if(!positionMarker && positionMarkerPrefab)
            {
                positionMarker = Instantiate(positionMarkerPrefab);
            }

            positionMarker.transform.position = retargettingPN.mainBodyPositionOffset;
        }
    }

    private void TogglePNControl()
    {
        retargettingPN.scriptEnabled = true;
        retargettingPN.ResetMainBodyPositionOffset(true);
        retargettingPN.useRootTranslation = true;
        GetComponent<Animator>().enabled = false;
        GetComponent<ThirdPersonUserControl>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void ToggleAnimControl()
    {
        retargettingPN.scriptEnabled = false;
        retargettingPN.useRootTranslation = false;
        GetComponent<Animator>().enabled = true;
        GetComponent<ThirdPersonUserControl>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
    }

}
