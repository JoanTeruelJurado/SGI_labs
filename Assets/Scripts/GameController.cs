using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance; // Singleton

    [Header("Gameplay Settings")]
    [SerializeField] private List<GameObject> fruitPrefabs;     // Lista de prefabs de frutas
    [SerializeField] private List<Transform> spawnPoints;       // Lista de puntos de spawn
    [SerializeField] private float minLaunchForce = 10f;        // Fuerza mínima
    [SerializeField] private float maxLaunchForce = 20f;        // Fuerza máxima
    [SerializeField] private float spawnInterval = 1.5f;        // Tiempo entre spawns
    [SerializeField] private VRHUDManager hudmanager;
    [SerializeField] private GameObject StartGamePanel;

    private int score = 0;
    private float timer = 0f;
    private bool gameStarted = false; // Control de inicio

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Desactiva el spawner hasta que empiece el juego
        CancelInvoke(nameof(SpawnFruit));
    }

    void Update()
    {
        // Solo cuenta tiempo si el juego ha empezado
        if (gameStarted)
        {
            timer += Time.deltaTime;
            SetTimer(timer);
        }
    }

    // =============================
    // ▶️ CONTROL DE JUEGO
    // =============================
    public void StartGame()
    {
        if (gameStarted) return; // Evita múltiples pulsaciones

        gameStarted = true;
        timer = 0f;
        score = 0;
        SetTimer(0.0f);
        setScore(0);
        // Oculta el panel de inicio
        if (StartGamePanel != null)
            StartGamePanel.SetActive(false);

        // Empieza el spawn loop
        InvokeRepeating(nameof(SpawnFruit), 1f, spawnInterval);

        Debug.Log("Juego iniciado.");
    }

    public void StopGame()
    {
        gameStarted = false;
        CancelInvoke(nameof(SpawnFruit));

        if (StartGamePanel != null)
            StartGamePanel.SetActive(true);

        Debug.Log("Juego detenido.");
    }

    // =============================
    // 🕒 TIMER
    // =============================
    public void SetTimer(float TIMELEFT)
    {
        hudmanager.setTime(TIMELEFT);
    }

    // =============================
    // 🧮 SCORE
    // =============================
    public int getScore() => score;
    
    public void setScore(int number) {
        score = number;
        hudmanager.setScore(score);
    }

    public void addScore(int number) {
        score += number;
        hudmanager.setScore(score);
    }

    // =============================
    // 🍉 FRUIT SPAWNER
    // =============================
    private void SpawnFruit()
    {
        if (!gameStarted) return; // No spawnear si no ha empezado el juego
        if (fruitPrefabs.Count == 0 || spawnPoints.Count == 0)
            return;

        GameObject fruitPrefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Count)];
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];

        GameObject fruit = Instantiate(fruitPrefab, spawn.position, spawn.rotation);

       Rigidbody rb = fruit.GetComponent<Rigidbody>();
       if (rb != null)
       {
           // Dirección del cañón = su eje Z positivo (forward)
           Vector3 launchDir = spawn.forward;

           // Puedes añadir una ligera variación aleatoria si quieres algo menos rígido
           launchDir = (launchDir + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.05f, 0.05f), 0f)).normalized;

           float force = Random.Range(minLaunchForce, maxLaunchForce);
           rb.AddForce(launchDir * force, ForceMode.Impulse);
       }

        Destroy(fruit, 10f);
    }

    // =============================
    // 🚪 EXIT
    // =============================
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Closing Application.");
    }
}
