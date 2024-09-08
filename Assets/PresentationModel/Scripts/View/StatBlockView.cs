using PresentationModel.Scripts.Presenter;
using TMPro;
using UnityEngine;

namespace PresentationModel.Scripts.View
{
    public class StatBlockView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Text;

        StatPresenter _presenter;
        public void Init(StatPresenter statPresenter)
        {
            _presenter = statPresenter;
            _presenter.OnStatChanged += StatValueChanged;
            _presenter.OnStatRemoved += HandleStatRemoved;
            GetStatText();
        }

        private void HandleStatRemoved()
        {
            OnDestroy();
        }

        private void StatValueChanged(int value)
        {
            GetStatText();
        }

        public void OnDestroy()
        {
            _presenter.OnStatChanged -= StatValueChanged;
            _presenter.OnStatRemoved -= OnDestroy;
            _presenter.Disable();
            Destroy(gameObject);
        }
    
        private void GetStatText()
        {
            Text.text = _presenter.Text;
        }
    }
}
