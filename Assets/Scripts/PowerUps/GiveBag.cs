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

            //TODO: don't copy, just take ? :)
            var actualSocket = bagsController.InstantiateWeapon(weapon.category, weapon.weaponPrefab);
            if (!actualSocket)
                return false;

            actualSocket.weapon.transform.position = transform.position;
            actualSocket.weapon.transform.rotation = transform.rotation;

            return true;
        }
    }

}