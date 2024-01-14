using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using CobaltCoreModding.Definitions;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using Microsoft.Extensions.Logging;
using KestrelMod.Artifacts;

namespace KestrelMod
{
    partial class KestrelManifest
    {
        public void PatchLaserMethod(Harmony harmony)
        {
            var patch_target = typeof(Ship).GetMethod("GetStatusSize", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic) ?? throw new Exception("method GetStatusSize not found!");
            var patch_method = typeof(KestrelManifest).GetMethod("PatchWeaponChargeBars", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public) ?? throw new Exception("method PatchWeaponChargeBars not found");
            harmony.Patch(patch_target, postfix: new HarmonyMethod(patch_method));
        }

        public static void PatchWeaponChargeBars(ref object __result)
        {
            var statusPlanType = AccessTools.Inner(typeof(Ship), "StatusPlan");
            var asBarsField = AccessTools.Field(statusPlanType, "asBars");
            var barMaxField = AccessTools.Field(statusPlanType, "barMax");
            asBarsField.SetValue(__result, true);
            barMaxField.SetValue(__result, 3);
        }

        
    }
}
