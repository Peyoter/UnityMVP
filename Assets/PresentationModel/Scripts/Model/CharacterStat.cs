using System;
using Sirenix.OdinInspector;

namespace PresentationModel.Scripts.Model
{
    public sealed class CharacterStat
    {
        public CharacterStat(string name, int value)
        {
            Name = name;
            Value = value;
        }
        
        public event Action<int> OnValueChanged; 

        [ShowInInspector, ReadOnly]
        public string Name { get; private set; }

        [ShowInInspector, ReadOnly]
        public int Value { get; private set; }

        [Button]
        public void ChangeValue(int value)
        {
            Value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}