using MBOptionScreen.Attributes;
using MBOptionScreen.Settings;

namespace HappyRebellion {
    public class SharedObjects : AttributeSettings<SharedObjects> {

        public override string Id { get; set; } = "heromal.HappyRebellion_v1";
        public override string ModName => "Happy Rebellion";
        public override string ModuleFolderName => "HappyRebellion";

        [SettingProperty("Enabled", hintText: "")]
        [SettingPropertyGroup("General")]
        public bool Enabled { get; set; } = true;

        [SettingProperty("Rebellion Relations Change", 0, 100, hintText: "Relation change when you do not give up your settlements when leaving. (Can be negative)")]
        [SettingPropertyGroup("General")]
        public int RebellionRelationsChange { get; set; } = 0;

        [SettingProperty("Forfeit Fiefs Relations Change", 0, 100, hintText: "Relation change when you give up your settlements when leaving. (Can be negative)")]
        [SettingPropertyGroup("General")]
        public int ForfeitSettlementsRelationsChange { get; set; } = 0;

        [SettingProperty("Declare War On Rebellion", hintText: "Does leaving a kingdom with your fiefs cause them to declare war on you?")]
        [SettingPropertyGroup("General")]
        public bool DeclareWarOnRebellion { get; set; } = false;
    }
}
