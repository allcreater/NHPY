using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TouchableObject
{
    public class GiveBag : GenericPowerUp
    {
        public StartWeapon weapon;

        protected override bool TryActivate(Collider other)
        {
            if (other.tag != pickerTag)
                return false;

            var bagsController = other.GetComponent<WeaponsController>();
            if (!bagsController)
                return false;

            var newBag = bagsController.AddWeapon(weapon.category, weapon.weaponPrefab);
            if (!newBag)
                return false;


            newBag.transform.position = transform.position;
            newBag.transform.rotation = transform.rotation;

            return true;
        }
    }

}