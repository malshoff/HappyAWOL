using HarmonyLib;
using System;
using System.Windows.Forms;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem.Actions;

namespace HappyRebellion {
    public class SubModule : MBSubModuleBase {
        
        protected override void OnSubModuleLoad() {
            //System.Diagnostics.Debug.WriteLine("Entered OnSubmoduleload");
            base.OnSubModuleLoad();
            

            System.Diagnostics.Debug.WriteLine(SharedObjects.Settings.ForfeitSettlementsRelationsChange.ToString());
            System.Diagnostics.Debug.WriteLine(SharedObjects.Settings.RebellionRelationsChange.ToString());
            //Type[] enumValue = typeof(ChangeKingdomAction).GetNestedTypes(System.Reflection.BindingFlags.NonPublic);

            /*
                foreach (var e in enumValue) {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            */

            Type type = typeof(ChangeKingdomAction).Assembly.GetType("TaleWorlds.CampaignSystem.Actions.ChangeKingdomAction+ChangeKingdomActionDetail");
            object val = (int)Enum.ToObject(type, 1);
            try {
                
                var h = new Harmony("happyrebellion");
                h.PatchAll();
                //MessageBox.Show("Loaded Harmony Patch");
                /*
                foreach (var method in methods) {
                    System.Diagnostics.Debug.WriteLine(method.ToString()); 
                }*/ 
            }
            catch (Exception e) {
                MessageBox.Show($"Error patching:\n{e.Message} \n\n{e.InnerException?.Message}");
            }

        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot() {
            InformationManager.DisplayMessage(new InformationMessage("HappyRebellion Loaded!", Color.FromUint(0xffbd_2b8d)));
        }
    }
}