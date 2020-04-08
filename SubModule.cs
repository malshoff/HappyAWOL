using HarmonyLib;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using Config.Net;
using HappyRebellion.Config;
using System.IO;
using TaleWorlds.Library;

namespace HappyRebellion {
    public class SubModule : MBSubModuleBase {
        
        protected override void OnSubModuleLoad() {
            //System.Diagnostics.Debug.WriteLine("Entered OnSubmoduleload");
            base.OnSubModuleLoad();
            

            System.Diagnostics.Debug.WriteLine(SharedObjects.Settings.ForfeitSettlementsRelationsChange.ToString());
            System.Diagnostics.Debug.WriteLine(SharedObjects.Settings.RebellionRelationsChange.ToString());

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