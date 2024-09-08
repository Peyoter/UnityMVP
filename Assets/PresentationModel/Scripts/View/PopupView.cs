using System.Collections.Generic;
using PresentationModel.Scripts.Presenter;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PresentationModel.Scripts.View
{
    public class PopupView : MonoBehaviour
    {

        [SerializeField] private Image Icon;
        [SerializeField] private TextMeshProUGUI  Name;
        [SerializeField] private TextMeshProUGUI  Description;
        [SerializeField] private TextMeshProUGUI  Level;
        [SerializeField] private TextMeshProUGUI  Xp;
        
        [SerializeField] private GameObject ButtonBlocked;
        [SerializeField] private GameObject ButtonActive;
        
        [SerializeField] private Button Button;
        
        [Inject]
        [ShowInInspector]
        private PopupPresenter _presenter;
        
        private HashSet<GameObject> _stats = new();
        private Button _btn;

        public void Show()
        {
            gameObject.SetActive(true);
            
            UpdateProgressBar();
            UpdateDescription();
            UpdateName();
            UpdateIcon();
            UpdateLevel();
            UpdateButtonState();
            
            _presenter.OnExperienceChanged += UpdateProgressBar;
            _presenter.OnIconChanged += UpdateIcon;
            _presenter.OnLvlUp += UpdateLevel;
            _presenter.OnNameChanged += UpdateName;
            _presenter.OnDescriptionChanged += UpdateDescription;
            _presenter.OnChangeButtonState += UpdateButtonState;
            
            Button.onClick.AddListener(OnButtonClicked);
        }

        private void UpdateButtonState()
        {
            if (_presenter.ButtonState)
            {
                ButtonBlocked.gameObject.SetActive(false);
                ButtonActive.gameObject.SetActive(true);
            }
            else {
                ButtonBlocked.gameObject.SetActive(true);
                ButtonActive.gameObject.SetActive(false);
            }
        }

        private void UpdateDescription()
        {
            Description.text = _presenter.Description;
        }

        private void UpdateName()
        {
            Name.text = _presenter.Name;
        }

        private void UpdateLevel()
        {
            Level.text = _presenter.CurrentLevel;
        }

        public void Hide()
        {
            foreach (var stat in _stats) {
                Destroy(stat);
            }
            
            _presenter.Hide();
            gameObject.SetActive(false);
            
            _presenter.OnExperienceChanged -= UpdateProgressBar;
            _presenter.OnIconChanged -= UpdateIcon;
            _presenter.OnLvlUp -= UpdateLevel;
            _presenter.OnNameChanged -= UpdateName;
            _presenter.OnDescriptionChanged -= UpdateDescription;
            
            Button.onClick.RemoveListener(OnButtonClicked);
        }

        private void UpdateIcon()
        {
            Icon.sprite = _presenter.Icon;
        }

        private void UpdateProgressBar()
        {
            Xp.text = _presenter.XpText;
        }

        private void OnButtonClicked()
        {
            _presenter.UpGrade();
        }

    }
}
