using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VRHUDManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI puntosText;
    [SerializeField] private TextMeshProUGUI tiempoText;

    [Header("Game Data")]
    public int puntos = 0;  // Aumenta esto desde otros scripts (ej: al recoger items)

    private float tiempo;

    public void setScore(int number) {
        puntos = number;
        // Actualiza puntos
        puntosText.text = "Puntos: " + puntos.ToString();
    }

    public void setTime(float time) {
        tiempo = time;
        string tiempoFormateado = FormatearTiempo(tiempo);
        tiempoText.text = "Tiempo: " + tiempoFormateado;
    }

    private void Awake()
    {
        // Inicia el tiempo al cargar la escena
        tiempo = Time.time;
    }

    private void Update()
    {
    }

    private string FormatearTiempo(float totalSegundos)
    {
        int minutos = Mathf.FloorToInt(totalSegundos / 60);
        int segundos = Mathf.FloorToInt(totalSegundos % 60);
        return string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    // Método público para añadir puntos (llámalo desde otros scripts)
    public void AnadirPuntos(int cantidad)
    {
        puntos += cantidad;
    }

    // Reinicia al cargar nueva escena (opcional)
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        tiempo = Time.time;
        puntos = 0;
    }
}