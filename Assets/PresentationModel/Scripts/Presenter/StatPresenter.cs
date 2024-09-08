using System;
using PresentationModel.Scripts.Model;
using UnityEngine;
using CharacterInfo = PresentationModel.Scripts.Model.CharacterInfo;

namespace PresentationModel.Scripts.Presenter
{
    public class StatPresenter
    {
        public string Text => $"{_name} : {_value}"; 
        private readonly string _name;
        private int _value;

        public event Action<int> OnStatChanged;
        public event Action OnStatRemoved;
        
        public CharacterStat CharacterStat;
        public CharacterInfo CharacterInfo;
        
        public StatPresenter(CharacterStat characterStat, CharacterInfo characterInfo)
        {
            CharacterStat = characterStat;
            CharacterInfo = characterInfo;
            _name = characterStat.Name;
            _value = characterStat.Value;
            
            CharacterStat.OnValueChanged += HandleChangeValue;
            CharacterInfo.OnStatRemoved += HandleStatRemove;
        }
        
        private void HandleStatRemove(CharacterStat stat)
        {
            if (stat.Name == _name) {
                OnStatRemoved?.Invoke();
            }
        }

        public void HandleChangeValue(int characterStatValue)
        {
            _value = characterStatValue;
            OnStatChanged?.Invoke(characterStatValue);
        }

        public void Disable()
        {
            CharacterStat.OnValueChanged -= HandleChangeValue;
            CharacterInfo.OnStatRemoved -= HandleStatRemove;
        }
    }
}