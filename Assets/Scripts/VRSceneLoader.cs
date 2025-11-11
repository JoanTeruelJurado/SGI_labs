using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Meta.XR;

public class VRSceneLoader : MonoBehaviour
{
    [SerializeField] private string targetSceneName = "EscenaDestino"; // Cambia por el nombre exacto
    [SerializeField] private string spawnPointTag = "SpawnPoint";     // Tag del punto de spawn

    private InputAction bButtonAction;
    private Transform playerRoot;

    private void Awake()
    {
        // Encuentra el root del jugador (normalmente el [BuildingBlock] Camera Rig)

        // Configura la acción del botón B (mando derecho)
        bButtonAction = new InputAction("BButton", binding: "<XRController>{RightHand}/secondaryButton");
        bButtonAction.performed += ctx => LoadTargetScene();
        bButtonAction.Enable();
    }

    private void OnDestroy()
    {
        bButtonAction.Disable();
    }

    private void LoadTargetScene()
    {
        // Guarda el nombre de la escena destino y el tag del spawn
        PlayerPrefs.SetString("TargetScene", targetSceneName);
        PlayerPrefs.SetString("SpawnTag", spawnPointTag);

        // Carga la escena
        SceneManager.LoadScene(targetSceneName);
    }
}