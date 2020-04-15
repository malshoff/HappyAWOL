using HarmonyLib;
using System;
using System.Diagnostics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;

namespace HappyRebellion {
    //[HarmonyPatch(typeof(ChangeKingdomAction), "ApplyByLeaveWithRebellionAgainstKingdom")]
    class PatchApplyByLeaveWithRebellionAgainstKingdom {
        public static bool Prefix(Clan clan, Kingdom newKingdom, bool showNotification = true) {
            var applyInternal = typeof(ChangeKingdomAction).GetMethod("ApplyInternal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            //Debug.WriteLine(applyInternal.Equals(null));
            try {
                applyInternal.Invoke(null, new object[] { clan, newKingdom, 3, 0, true, showNotification });
            }
            catch (Exception ) {
                return false;
            }
            return false;
        }
    }
}