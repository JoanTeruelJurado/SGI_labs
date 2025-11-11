using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timer;
    public void SetTimer(float TIMELEFT)
    {
        int minutes, seconds, cents;
        minutes = (int)(TIMELEFT / 60f);
        seconds = (int)(TIMELEFT - minutes * 60f);
        cents = (int)((TIMELEFT - (int)TIMELEFT) * 100f);

        _timer.SetText("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Closing Application.");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
