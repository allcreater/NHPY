using UnityEngine;
using System.Linq;

namespace TouchableObject
{
    public class GenericPowerUp : MonoBehaviour
    {
        public string pickerTag;

        //private AudioSource audioSource;

        //private void Awake() //TODO: inherit from common PowerUp 
        //{
        //    audioSource = GetComponent<AudioSource>();
        //}

        protected virtual bool TryActivate(Collider other) { return false; }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag != pickerTag)
                return;

            var isActionPerformed = TryActivate(other);
            if (isActionPerformed)
            {
                gameObject.SetActive(false);
                GameObject.Destroy(gameObject);
            }
        }
    }
}
