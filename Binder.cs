using System.Collections;
using System.Linq;
using UnityEngine;
using Landfall.TABS;
using DM;

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
            var db = ContentDatabase.Instance().LandfallContentDatabase;
            var club = db.GetWeapons().ToList().Find(x => x.name.Equals("Club_1 Weapons_VB"));
            foreach (var bp in db.GetUnitBlueprints().ToList())
            {
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