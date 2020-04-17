using HarmonyLib;
using System;
using System.Windows.Forms;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;


namespace HappyRebellion {
    public class SubModule : MBSubModuleBase {

        protected override void OnSubModuleLoad() {
            base.OnSubModuleLoad();

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