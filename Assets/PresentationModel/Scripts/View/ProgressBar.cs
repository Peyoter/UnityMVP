using System;
using PresentationModel.Scripts.Presenter;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PresentationModel.Scripts.View
{
    enum ProgressBarType {
        Completed,
        NotCompled,
    }
    
    public class ProgressBar : MonoBehaviour
    {
        public int Max;
        public int Current;
        
        [SerializeField] private Image Mask;
        [SerializeField] private ProgressBarType Type;

        [Inject] private PopupPresenter _presenter;

        private void OnEnable()
        {
            UpdateProgressBar();
            _presenter.OnExperienceChanged += UpdateProgressBar;
        }
        
        private void OnDisable()
        {
            UpdateProgressBar();
            _presenter.OnExperienceChanged += UpdateProgressBar;
        }

        private void UpdateProgressBar()
        {
            Current = _presenter.Xp;
            Max = _presenter.Rxp;
            
            // Можно убрать в презентер но сил уже нет)
            if (_presenter.ButtonState && Type == ProgressBarType.Completed && !gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            
            if (!_presenter.ButtonState && Type == ProgressBarType.NotCompled && !gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            
            if (_presenter.ButtonState && Type == ProgressBarType.NotCompled && gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
            
            if (!_presenter.ButtonState && Type == ProgressBarType.Completed && gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }

        void Update()
        {
            GetCurrentFill();
        }

        private void GetCurrentFill()
        {
            float fillAmount = (float)Current / (float)Max;
            Mask.fillAmount = fillAmount;
        }
    }
}