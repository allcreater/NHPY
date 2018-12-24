using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Preferences
{
    public enum DynamicLightsAmount
    {
        OnlyImportant,
        Flashes,
        Everywhere
    }

    public class GameSettings : MonoBehaviour
    {
        public static GameSettings instance = null;

        public float difficulty
        {
            get;
            set;
        } = 0.5f;

        public DynamicLightsAmount dynamicLightsAmount
        {
            get;
            set;
        } = DynamicLightsAmount.OnlyImportant;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance == this) // Экземпляр объекта уже существует на сцене
                Destroy(gameObject);

            // Теперь нам нужно указать, чтобы объект не уничтожался
            // при переходе на другую сцену игры
            DontDestroyOnLoad(gameObject);

            // И запускаем собственно инициализатор
            InitializeManager();
        }

        public void Set()
        {
            PlayerPrefs.SetFloat(nameof(difficulty), difficulty);
            PlayerPrefs.SetString(nameof(dynamicLightsAmount), dynamicLightsAmount.ToString());

            PlayerPrefs.Save();
        }

        private void InitializeManager()
        {
            difficulty = PlayerPrefs.GetFloat(nameof(difficulty), difficulty);

            if (System.Enum.TryParse(PlayerPrefs.GetString(nameof(dynamicLightsAmount)), out DynamicLightsAmount parsedEnum))
                dynamicLightsAmount = parsedEnum;
        }
    }

}