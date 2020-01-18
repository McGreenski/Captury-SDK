using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FocusCamera : MonoBehaviour
{
    public CinemachineFreeLook freeLookVCam;
    public GameObject[] PlayerCharacter;
    public bool CheckForCharacter;
    public float MouseScroll;
    public float ZoomValue;
    public bool LeftClickPressed;
    public bool Init;
    // Update is called once per frame

    void Start()

    {
        ZoomValue = freeLookVCam.m_Lens.FieldOfView;
    }


    public void CheckForCharacterValue()
    {
       
        Invoke("UpdateBool", 1);
    }

    void UpdateBool()
    {
       
        CheckForCharacter = true;
        LeftClickPressed = false;
    }


    void LateUpdate() 
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            LeftClickPressed = true;
        }

        if (Input.GetMouseButtonUp(0) == true)
        {
            LeftClickPressed = false;
            Debug.Log("Up!!!");
        }


        MouseScroll = Input.mouseScrollDelta.y;
    }

    public void Update()
    {
        if (CheckForCharacter == true)
        {
            if (freeLookVCam.Follow == null)
            {
                PlayerCharacter = GameObject.FindGameObjectsWithTag("Player");
                freeLookVCam.Follow = PlayerCharacter[0].transform;
                freeLookVCam.LookAt = PlayerCharacter[0].transform;
                Debug.Log("Found Player");
                CheckForCharacter = false;
            }

        }


        if (LeftClickPressed == true)
        {
            freeLookVCam.enabled = true;
        }


        if (MouseScroll > 0.1f)
        {
            freeLookVCam.enabled = true;
            ZoomValue = freeLookVCam.m_Lens.FieldOfView + 2;
            freeLookVCam.m_Lens.FieldOfView = ZoomValue;
        }
        else if (MouseScroll < -0.1f)
        {
            freeLookVCam.enabled = true;
            ZoomValue = freeLookVCam.m_Lens.FieldOfView - 2;
            freeLookVCam.m_Lens.FieldOfView = ZoomValue;
        }
        else
        {
            MouseScroll = 0;
        }
        if (Init == false)
        {
            if (MouseScroll == 0 & LeftClickPressed == false)
            {
                freeLookVCam.enabled = false;
            }
        }
    }

    }



