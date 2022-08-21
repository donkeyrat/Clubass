using System.Collections;
using System.Linq;
using UnityEngine;
using Landfall.TABS;

namespace Clubass
{
    public class Binder : MonoBehaviour
    {
        public static void UnitGlad()
        {
            if (!instance)
            {
                instance = new GameObject
                {
                    hideFlags = HideFlags.HideAndDontSave
                }.AddComponent<Binder>();
            }
            instance.StartCoroutine(StartUnitgradLate());
        }

        private static IEnumerator StartUnitgradLate()
        {
            yield return new WaitUntil(() => FindObjectOfType<ServiceLocator>() != null);
            yield return new WaitUntil(() => ServiceLocator.GetService<ISaveLoaderService>() != null);
            yield return new WaitForSeconds(1f);
            var database = LandfallUnitDatabase.GetDatabase();
            var club = database.WeaponList.ToList().Find(x => x.name.Equals("Club_1 Weapons_VB"));
            foreach (IDatabaseEntity databaseEntity in database.UnitList)
            {
                var bp = (UnitBlueprint)databaseEntity;
                if (!bp.UnitBase) continue;
                if (bp.UnitBase.GetComponent<Unit>().unitType == Unit.UnitType.Warmachine && !bp.UnitBase.name.Contains("DaVinciTank")) continue;
                if (bp.UnitBase.GetComponentInChildren<HandRight>()) { bp.RightWeapon = club; }
                if (bp.UnitBase.GetComponentInChildren<HandLeft>()) { bp.LeftWeapon = club; }
            }
            yield break;
        }

        private static Binder instance;
    }
}