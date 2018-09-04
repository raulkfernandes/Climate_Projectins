using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

    private UpdateClimateData climateData;

    private void Start()
    {
        //this.LockCursor();
        climateData = FindObjectOfType<UpdateClimateData>();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void FinishAnimation()
    {
        climateData.animacaoEncerrada = true;
    }
}
