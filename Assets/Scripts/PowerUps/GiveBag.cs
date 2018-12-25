using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TouchableObject
{

    public class GiveBag : MonoBehaviour
    {
        public string pickerTag;
        public int bagIndex = 0;

        //private AudioSource audioSource;

        //private void Awake() //TODO: inherit from common PowerUp 
        //{
        //    audioSource = GetComponent<AudioSource>();
        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != pickerTag)
                return;

            gameObject.SetActive(false);

            var bagsController = other.GetComponent<BagsController>();
            if (bagsController)
            {
                var newBag = bagsController.AddBag(bagIndex);
                if (newBag)
                {
                    newBag.transform.position = transform.position;
                    newBag.transform.rotation = transform.rotation;
                }
            }

            GameObject.Destroy(gameObject);
        }
    }

}