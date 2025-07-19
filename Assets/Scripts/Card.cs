using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KuroProductions
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public List<CardType> cardType;
        public int health;
        public int damageMin;
        public int damageMax;
        public List<DamageType> damageType;

        public enum CardType
        {
            Fire,
            Water,
            Earth,
            Air,
            Light,
            Dark
        }

        public enum DamageType
        {
            Fire,
            Water,
            Earth,
            Air,
            Light,
            Dark
        }
    }
}
