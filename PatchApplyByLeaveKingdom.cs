using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;


namespace HappyRebellion {
    [HarmonyPatch(typeof(ChangeKingdomAction), "ApplyByLeaveKingdom")]
    class PatchApplyByLeaveKingdom {

        public static bool Prefix(Clan clan, bool showNotification = true) {
            try {
                var applyInternal = typeof(ChangeKingdomAction).GetMethod("ApplyInternal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
                applyInternal.Invoke(null, new object[] { clan, clan.Kingdom, 2, 0, false, showNotification });
            }
            catch (Exception) {
                return false;
            }
            return false;
        }
    }
}