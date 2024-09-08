using PresentationModel.Scripts.Data;
using PresentationModel.Scripts.Presenter;
using PresentationModel.Scripts.View;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace PresentationModel.Scripts
{
    public class PopupManager : MonoBehaviour
    {

        [SerializeField] private CharacterObject CharacterObject;
        
        [Inject] private PopupView _popupView;
        [Inject] private PopupPresenter _popupPresenter;
        
        [Button]
        private void ShowPopup()
        {
            _popupPresenter.PopupInitDataFromObject(CharacterObject);
            _popupView.Show();
        }
        
        [Button]
        private void HidePopup()
        {
            _popupView.Hide();
        }
    }
}