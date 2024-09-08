using System;
using PresentationModel.Scripts.Data;
using PresentationModel.Scripts.Fabric;
using PresentationModel.Scripts.Model;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using CharacterInfo = PresentationModel.Scripts.Model.CharacterInfo;

namespace PresentationModel.Scripts.Presenter
{
    [Serializable]
    public class PopupPresenter
    {
        public Sprite Icon => _userInfoModel.Icon;
        public string Name => _userInfoModel.Name;
        public string Description => _userInfoModel.Description;
        public string CurrentLevel => $"Level: {_playerLevel.CurrentLevel.ToString()}";
        
        public string XpText => $"Exp: {Xp.ToString()} / {Rxp}";
        
        public int Xp => _playerLevel.CurrentExperience;
        
        public int Rxp => _playerLevel.RequiredExperience;

        public CharacterStat[] Stats => _characterInfo.GetStats();

        [ShowInInspector]
        private CharacterInfo _characterInfo;
        
        [ShowInInspector]
        private PlayerLevel _playerLevel;
        
        [ShowInInspector]
        private UserInfoModel _userInfoModel;

        public Boolean ButtonState => _playerLevel.CanLevelUp();
        public event Action OnChangeButtonState;
        
        public event Action OnIconChanged;
        public event Action OnExperienceChanged;
        public event Action OnLvlUp;
        public event Action OnNameChanged;
        public event Action OnDescriptionChanged;
        
        private StatBlockFabric _statBlockFabric;

        [Inject]
        public PopupPresenter(
                UserInfoModel userInfoModel, 
                PlayerLevel playerLevel, 
                CharacterInfo characterInfo,
                StatBlockFabric statBlockFabric
            )
        {
            _userInfoModel = userInfoModel;
            _playerLevel = playerLevel;
            _characterInfo = characterInfo;
            _statBlockFabric = statBlockFabric;
            _userInfoModel.OnDescriptionChanged += HandleDescriptionChanged;
            _userInfoModel.OnIconChanged += HandleIconChanged;
            _userInfoModel.OnNameChanged += HandleNameChanged;
            _playerLevel.OnLevelUp += HandleLvlUp;
            _playerLevel.OnExperienceChanged += HandleExperienceChanged;
            _characterInfo.OnStatAdded += HandleStatAdded;
        }

        private void HandleStatAdded(CharacterStat stat)
        {
            var statModel = _characterInfo.GetStat(stat.Name);
            _statBlockFabric.Create(statModel, _characterInfo);
        }

        public void PopupInitDataFromObject(CharacterObject characterObject)
        {
            _userInfoModel.ChangeName(characterObject.Name);
            _userInfoModel.ChangeDescription(characterObject.Description);
            _userInfoModel.ChangeIcon(characterObject.Image);
            
            _playerLevel.AddExperience(characterObject.Xp);
            
            foreach (var characterObjectAttribute in characterObject.Attributes)
            {
                var stat = new CharacterStat(characterObjectAttribute.Name, characterObjectAttribute.Value);
                _characterInfo.AddStat(stat);
            }
            
        }

        public void Hide()
        {
            _playerLevel.Clear();
            _characterInfo.RemoveAllStat();
        }
        
        private void HandleIconChanged(Sprite sprite)
        {
            OnIconChanged?.Invoke();
        }

        private void HandleDescriptionChanged(string name)
        {
            OnDescriptionChanged?.Invoke();
        }  
        
        private void HandleNameChanged(string name)
        {
            OnNameChanged?.Invoke();
        }
        
        private void HandleExperienceChanged()
        {
            OnExperienceChanged?.Invoke();
            if (_playerLevel.CanLevelUp()) {
                OnChangeButtonState?.Invoke();
            }
        }

        private void HandleLvlUp()
        {
            OnLvlUp?.Invoke();
            OnExperienceChanged?.Invoke();
            OnChangeButtonState?.Invoke();
        }

        public void UpGrade()
        {
            _playerLevel.LevelUp();
        }
    }
}