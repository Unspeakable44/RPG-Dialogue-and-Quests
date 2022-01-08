﻿using GameDevTV.Saving;
using GameDevTV.Utils;
using RPG.Stats;
using System.Collections;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour, ISaveable
    {

        // state
        LazyValue<float> mana;

        private void Awake()
        {
            mana = new LazyValue<float>(GetMaxMana);
        }

        private void Update()
        {
            if (mana.value < GetMaxMana())
            {
                mana.value += GetRegenRate() * Time.deltaTime;

                if (mana.value > GetMaxMana())
                {
                    mana.value = GetMaxMana();
                }
            }
        }

        public float GetCurrentMana()
        {
            return mana.value;
        }

        public float GetMaxMana()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Mana);
        }

        public float GetRegenRate()
        {
            return GetComponent<BaseStats>().GetStat(Stat.ManaRegenRate);
        }

        public bool UseMana(float manaToUse)
        {
            if (manaToUse > mana.value) return false;
            mana.value -= manaToUse;
            return true;
        }

        public object CaptureState()
        {
            return mana.value;
        }

        public void RestoreState(object state)
        {
            mana.value = (float)state;
        }
    }
}