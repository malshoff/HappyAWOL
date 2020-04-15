using HarmonyLib;
using Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Library;

namespace HappyRebellion {
   

    [HarmonyPatch(typeof(ChangeKingdomAction), "ApplyInternal")]
    public class PatchRelationPenalty {
       

        static bool Prefix(Clan clan, Kingdom kingdom, int detail, int awardMultiplier, bool byRebellion, bool showNotification) {
            //MessageBox.Show($"Detail: {detail}");

            var onClanChangedKingdom = typeof(CampaignEventDispatcher).GetMethod("OnClanChangedKingdom", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var onMercenaryClanChangedKingdom = typeof(CampaignEventDispatcher).GetMethod("OnMercenaryClanChangedKingdom", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Type type = typeof(ChangeKingdomAction).Assembly.GetType("TaleWorlds.CampaignSystem.Actions.ChangeKingdomAction+ChangeKingdomActionDetail");
            int joinMerc = (int)Enum.ToObject(type, 0);
            int joinKingdom = (int)Enum.ToObject(type, 1);
            int leaveKingdom = (int)Enum.ToObject(type, 2);
            int leaveRebellion = (int)Enum.ToObject(type, 3);
            int leaveMerc = (int)Enum.ToObject(type, 4);
           
            Kingdom oldKingdom = clan.Kingdom;
            if (kingdom != null) {
                foreach (Kingdom kingdom3 in Kingdom.All) {
                    if (object.ReferenceEquals(kingdom3, kingdom) || !kingdom.IsAtWarWith(kingdom3)) {
                        FactionHelper.FinishAllRelatedHostileActionsOfFactionToFaction(clan, kingdom3);
                        FactionHelper.FinishAllRelatedHostileActionsOfFactionToFaction(kingdom3, clan);
                    }
                }
                foreach (Clan clan2 in Clan.All) {
                    if (!object.ReferenceEquals(clan2, clan) && ((clan2.Kingdom == null) && !kingdom.IsAtWarWith(clan2))) {
                        FactionHelper.FinishAllRelatedHostileActions(clan, clan2);
                    }
                }
            }
            if (detail == joinKingdom) { //ChangeKingdomActionDetail.JoinKingdom
                object[] additionalArgs = new object[] { clan, oldKingdom, kingdom, false };
                StatisticsDataLogHelper.AddLog(StatisticsDataLogHelper.LogAction.ChangeKingdomAction, additionalArgs);
                clan.IsUnderMercenaryService = false;
                if (oldKingdom != null) {
                    clan.ClanLeaveKingdom(!byRebellion);
                }
                clan.ClanJoinFaction(kingdom);
                onClanChangedKingdom.Invoke(CampaignEventDispatcher.Instance, new object[] { clan, oldKingdom, clan.Kingdom, byRebellion, showNotification });
            }
            else if (detail == joinMerc) { //ChangeKingdomActionDetail.JoinAsMercenary
                object[] additionalArgs = new object[] { clan, oldKingdom, kingdom, false };
                StatisticsDataLogHelper.AddLog(StatisticsDataLogHelper.LogAction.ChangeKingdomAction, additionalArgs);
                if (clan.IsUnderMercenaryService) {
                    clan.ClanLeaveKingdom(false);
                }
                clan.MercenaryAwardMultiplier = MathF.Round((float)awardMultiplier);
                clan.IsUnderMercenaryService = true;
                clan.ClanJoinFaction(kingdom);
                //CampaignEventDispatcher.Instance.OnMercenaryClanChangedKingdom(clan, null, kingdom);
                onMercenaryClanChangedKingdom.Invoke(CampaignEventDispatcher.Instance, new object[] { clan, null, kingdom });
            }
            else if (detail == leaveRebellion || detail == leaveKingdom || detail == leaveMerc ){ //ChangeKingdomActionDetail.LeaveAsMercenary = 4
                object[] additionalArgs = new object[] { clan, oldKingdom, kingdom, true };
                StatisticsDataLogHelper.AddLog(StatisticsDataLogHelper.LogAction.ChangeKingdomAction, additionalArgs);
                clan.ClanLeaveKingdom(false);
                if (detail == leaveMerc) { //ChangeKingdomActionDetail.LeaveAsMercenary
                    onMercenaryClanChangedKingdom.Invoke(CampaignEventDispatcher.Instance, new object[] { clan, kingdom, null });
                    clan.IsUnderMercenaryService = false;
                }
                if (detail == leaveRebellion ) { //ChangeKingdomActionDetail.LeaveWithRebellion
                    if (object.ReferenceEquals(clan, Clan.PlayerClan)) {
                        foreach (Clan clan3 in oldKingdom.Clans) {
                            ChangeRelationAction.ApplyRelationChangeBetweenHeroes(clan.Leader, clan3.Leader, SharedObjects.Settings.RebellionRelationsChange, true);
                        }
                        if(SharedObjects.Settings.DeclareWarOnRebellion)
                            DeclareWarAction.Apply(oldKingdom, clan);
                    }
                    onClanChangedKingdom.Invoke(CampaignEventDispatcher.Instance, new object[] { clan, oldKingdom, null, true,true });
                }
                else if (detail == leaveKingdom) { //ChangeKingdomActionDetail.LeaveKingdom
                    if (object.ReferenceEquals(clan, Clan.PlayerClan)) {
                        foreach (Clan clan4 in oldKingdom.Clans) {
                            ChangeRelationAction.ApplyRelationChangeBetweenHeroes(clan.Leader, clan4.Leader, SharedObjects.Settings.ForfeitSettlementsRelationsChange, true);
                        }
                    }
                    foreach (Settlement settlement in new List<Settlement>(clan.Settlements)) {
                        ChangeOwnerOfSettlementAction.ApplyByLeaveFaction(oldKingdom.Leader, settlement);
                        foreach (Hero hero in new List<Hero>((IEnumerable<Hero>)settlement.HeroesWithoutParty)) {
                            if ((hero.CurrentSettlement != null) && object.ReferenceEquals(hero.Clan, clan)) {
                                if (hero.PartyBelongedTo != null) {
                                    LeaveSettlementAction.ApplyForParty(hero.PartyBelongedTo);
                                    EnterSettlementAction.ApplyForParty(hero.PartyBelongedTo, clan.Leader.HomeSettlement);
                                    continue;
                                }
                                LeaveSettlementAction.ApplyForCharacterOnly(hero);
                                EnterSettlementAction.ApplyForCharacterOnly(hero, clan.Leader.HomeSettlement);
                            }
                        }
                    }
                    onClanChangedKingdom.Invoke(CampaignEventDispatcher.Instance, new object[] { clan,oldKingdom, null, false, false });
                }
            }
            if (object.ReferenceEquals(clan, Clan.PlayerClan)) {
                Campaign.Current.UpdateDecisions();
            }
            typeof(ChangeKingdomAction).GetMethod("CheckIfPartyIconIsDirty", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
            .Invoke(null, new object[] { clan, kingdom });
            return false;
        }

    }
}
