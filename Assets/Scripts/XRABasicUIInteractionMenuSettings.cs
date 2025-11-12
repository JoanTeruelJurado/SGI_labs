using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // <= necesario para Slider, Button, etc.
using TMPro;           // <= opcional si usas TextMeshPro

// Poner un namespace evita colisiones de nombres entre scripts
namespace TetrisVR.UI
{
    public class VRSettingsMenu : MonoBehaviour
    {
        [Header("Volume")]
        public Slider volumeSlider;

        [Header("Difficulty Buttons")]
        public Button easyButton;
        public Button mediumButton;
        public Button hardButton;

        [Header("Close")]
        public Button closeButton;

        // Si no usas TextMeshPro para labels, comenta estas líneas o cámbialas
        public TMP_Text titleText; // opcional

        private enum Difficulty { Easy = 0, Medium = 1, Hard = 2 }
        private Difficulty currentDifficulty;

        void Awake()
        {
            // Protecciones ante referencias nulas para evitar errores en tiempo de ejecución
            if (volumeSlider == null) Debug.LogWarning("VolumeSlider no asignado en VRSettingsMenu.");
            if (easyButton == null || mediumButton == null || hardButton == null) Debug.LogWarning("Alguno de los botones de dificultad no está asignado.");
            if (closeButton == null) Debug.LogWarning("CloseButton no asignado.");
        }

        void Start()
        {
            // Volumen
            float savedVolume = PlayerPrefs.GetFloat("volume", 0.5f);
            if (volumeSlider != null)
            {
                volumeSlider.value = savedVolume;
                volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            }
            AudioListener.volume = savedVolume;

            // Dificultad - listeners
            if (easyButton != null) easyButton.onClick.AddListener(() => SetDifficulty(Difficulty.Easy));
            if (mediumButton != null) mediumButton.onClick.AddListener(() => SetDifficulty(Difficulty.Medium));
            if (hardButton != null) hardButton.onClick.AddListener(() => SetDifficulty(Difficulty.Hard));

            // Cargar dificultad guardada y aplicar (default = Easy)
            int savedDiff = PlayerPrefs.GetInt("difficulty", (int)Difficulty.Easy);
            SetDifficulty((Difficulty)Mathf.Clamp(savedDiff, 0, 2));

            // Cerrar panel
            if (closeButton != null) closeButton.onClick.AddListener(CloseMenu);
        }

        private void OnVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("volume", value);
            PlayerPrefs.Save();
            AudioListener.volume = value;
        }

        private void SetDifficulty(Difficulty diff)
        {
            currentDifficulty = diff;
            PlayerPrefs.SetInt("difficulty", (int)diff);
            PlayerPrefs.Save();

            // Feedback visual simple (puedes reemplazar con animaciones, glow, etc)
            ResetButtonsVisual();

            if (easyButton != null && mediumButton != null && hardButton != null)
            {
                switch (diff)
                {
                    case Difficulty.Easy:
                        SetButtonSelectedVisual(easyButton);
                        break;
                    case Difficulty.Medium:
                        SetButtonSelectedVisual(mediumButton);
                        break;
                    case Difficulty.Hard:
                        SetButtonSelectedVisual(hardButton);
                        break;
                }
            }

            // Aquí puedes notificar a tu GameManager para aplicar la dificultad real
            // GameManager.Instance.SetDifficulty((int)diff);
        }

        private void ResetButtonsVisual()
        {
            if (easyButton != null) ResetButtonVisual(easyButton);
            if (mediumButton != null) ResetButtonVisual(mediumButton);
            if (hardButton != null) ResetButtonVisual(hardButton);
        }

        private void ResetButtonVisual(Button b)
        {
            var img = b.image;
            if (img != null) img.color = Color.white;
        }

        private void SetButtonSelectedVisual(Button b)
        {
            var img = b.image;
            if (img != null) img.color = new Color(0.8f, 0.9f, 1f); // color suave para seleccionado
        }

        private void CloseMenu()
        {
            // Puedes desactivar el panel completo (este script asume que está en el root del panel)
            gameObject.SetActive(false);
        }
    }
}
