using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    // public bool active;
    private bool active = true;
    
    // returns active status
    public bool IsActive(){
        return active;
    }

    // sets start screen active
    public void Setup() {
        gameObject.SetActive(true);
    }

    // hides the start screen; called by the Play Button in start screen
    public void Hide() {
        gameObject.SetActive(false);
        active = false;
    }
}
