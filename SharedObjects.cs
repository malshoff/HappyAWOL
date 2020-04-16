using MBOptionScreen.Attributes;
using MBOptionScreen.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaleWorlds.Library;

namespace HappyRebellion {
    public class SharedObjects: AttributeSettings<SharedObjects> {
        
        public override string Id { get; set; } = "heromal.HappyRebellion_v1";
        public override string ModName => "Hsppy Rebellion";
        public override string ModuleFolderName => "heromal.HappyRebellion";

        [SettingProperty("Enabled", "")]
        [SettingPropertyGroup("General")]
        public bool Enabled { get; set; } = true;

        [SettingProperty("Rebellion Relations Change", "Relation change when you do not give up your settlements when leaving. (Can be negative)")]
        [SettingPropertyGroup("General")]
        public int RebellionRelationsChange { get; set; } = 0;

        [SettingProperty("Forfeit Fiefs Relations Change", "Relation change when you give up your settlements when leaving. (Can be negative)")]
        [SettingPropertyGroup("General")]
        public int ForfeitSettlementsRelationsChange { get; set; } = 0;

        [SettingProperty("Declare War On Rebellion", "Does leaving a kingdom with your fiefs cause them to declare war on you?")]
        [SettingPropertyGroup("General")]
        public bool DeclareWarOnRebellion { get; set; } = false;


    }
}
