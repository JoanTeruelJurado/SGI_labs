using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Meta.XR;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static TutorialController Instance; // Singleton
    [SerializeField] private string TutorialScene = "Game"; // Cambia por el nombre exacto
    [SerializeField] private string GameScene = "Tutorial";     // Tag del punto de spawn
    [SerializeField] private GameObject SettingMenu;
    private InputAction bButtonAction;
    private bool menuopen = false; 
    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            menuopen = false; 
            DontDestroyOnLoad(gameObject);
            SettingMenu.SetActive(false);
            bButtonAction = new InputAction("BButton", binding: "<XRController>{RightHand}/secondaryButton");
            bButtonAction.performed += ctx => toggleMenu();
            bButtonAction.Enable();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // =============================
    // 🚪 EXIT
    // =============================
    public void QuitApp() {
        Application.Quit();
        Debug.Log("Closing Application.");
    }

    public void GoGame() {
        // Carga la escena
        SceneManager.LoadScene(GameScene);
    }

    public void GoTutorial() {
        // Carga la escena
        SceneManager.LoadScene(TutorialScene);
    }

    void toggleMenu() {
        if (menuopen) {
            SettingMenu.SetActive(false);
            menuopen = false;
        }
        else  {
            SettingMenu.SetActive(true);
            menuopen = true;
        }
    }
}
