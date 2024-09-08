using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace PresentationModel.Scripts.Data
{
    [Serializable]
    public class Attributes
    {
        public string Name;
        public int Value;
    }
    
    [CreateAssetMenu]
    public class CharacterObject : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Image;
        public int Xp;
        public List<Attributes> Attributes;
    }
}