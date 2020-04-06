using HarmonyLib;
using Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Library;

namespace HappyRebellion {
    [HarmonyPatch(typeof(ChangeKingdomAction), "ApplyByJoinFactionAsMercenary")]
    class PatchApplyMercernaryJoin {

        public static bool Prefix(Clan clan, Kingdom newKingdom, int awardMultiplier = 50, bool showNotification = true) {

            try {
                var applyInternal = typeof(ChangeKingdomAction).GetMethod("ApplyInternal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
                applyInternal.Invoke(null, new object[] { clan, newKingdom, 0, awardMultiplier, false, showNotification });
            }
            catch (Exception e) {
                return false;
            }
            return false;
        }
    }
}
