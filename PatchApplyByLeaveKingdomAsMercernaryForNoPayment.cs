using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;


namespace HappyRebellion {
    [HarmonyPatch(typeof(ChangeKingdomAction), "ApplyByLeaveKingdomAsMercenaryForNoPayment")]
    class PatchApplyByLeaveKingdomAsMercernaryForNoPayment {

        public static bool Prefix(Clan mercenaryClan, Kingdom kingdom, bool showNotification = true) {
            try {
                var applyInternal = typeof(ChangeKingdomAction).GetMethod("ApplyInternal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
                applyInternal.Invoke(null, new object[] { mercenaryClan, kingdom, 4, 0, false, showNotification });
            }
            catch (Exception) {
                return false;
            }
            return false;
        }
    }
}