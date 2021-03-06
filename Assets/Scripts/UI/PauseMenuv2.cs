 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.InputSystem;
 
 public class PauseMenuv2 : MonoBehaviour
 {
     public static bool GameIsPaused = false;
     public GameObject pauseMenuUI;
     public InputAction pauseControl;

     private void OnEnable()
     {
         pauseControl.Enable();
     }

     private void OnDisable()
     {
         pauseControl.Disable();
     }

     void Update()
     {
         if(pauseControl.triggered)
         {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            } 
         }
     }
 
    public void Resume()
     {
         pauseMenuUI.SetActive(false);
         Time.timeScale = 1f;
         GameIsPaused = false;
     }
     void Pause()
     
     {
         pauseMenuUI.SetActive(true);
         Time.timeScale = 0f;
         GameIsPaused = true;
     }
 }