
using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;


namespace HappyRebellion {
    [HarmonyPatch(typeof(ChangeKingdomAction), "ApplyByJoinToKingdom")]
    class PatchClanJoinsKingdom {

        public static bool Prefix(Clan clan, Kingdom newKingdom, bool showNotification = true) {
            try {
                var applyInternal = typeof(ChangeKingdomAction).GetMethod("ApplyInternal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
                applyInternal.Invoke(null, new object[] { clan, newKingdom, 1, 0, false, showNotification });

            }
            catch (Exception ) {
                return false;
            }
            return false;
        }
    }
}

