using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modding;
using UnityEngine;
using Vasi;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker;
namespace ChangeHud
{
    public class ChangeHud:Mod,IGlobalSettings<GlobalSetting>,IMenuMod
    {
        public override string GetVersion()
        {
            return "1.0";
        }
        public static GlobalSetting GS = new GlobalSetting();
        public GlobalSetting OnSaveGlobal() => GS;
        public void OnLoadGlobal(GlobalSetting s) => GS = s;
        public override void Initialize()
        {
            On.PlayMakerFSM.Start += Change;
        }
        public bool ToggleButtonInsideMenu => false;
        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? menu)
        {
            List<IMenuMod.MenuEntry> menuEntries = new List<IMenuMod.MenuEntry>();
            menuEntries.Add(
                new IMenuMod.MenuEntry
                {
                    Name="Theme",
                    Description="Choose the theme of hud you want",
                    Values=Enum.GetNames(typeof(GlobalSetting.Mode)).ToArray(),
                    Saver=i=>GS.usemode=(GlobalSetting.Mode)i,
                    Loader=()=>(int)GS.usemode
                }
                );
            return menuEntries;
        }
        private void Change(On.PlayMakerFSM.orig_Start orig, PlayMakerFSM self)
        {
            orig(self);
            
            if(self.gameObject.name=="HUD_frame"&&self.FsmName=="Load Animation")
            {
                Log("Find the Hud");
                bool IsGodSeeker = false;
                bool Issteel = false;
                if (GS.usemode == GlobalSetting.Mode.GodSeeker)
                {
                    IsGodSeeker = true;
                }
                if (GS.usemode == GlobalSetting.Mode.Steel)
                {
                    Issteel = true;
                }
                var Judge = self.GetState("Set Anims");
                FsmEvent ggmode = self.FsmEvents.FirstOrDefault(x => x.Name == "GG MODE");
                Judge.RemoveAction(0);
                Judge.InsertAction(0,new BoolTest()
                {
                    boolVariable = new FsmBool(IsGodSeeker),
                    isTrue = ggmode
                }) ;
                Judge.RemoveAction(6);
                Judge.InsertAction(6,new BoolTest()
                {
                    boolVariable =new FsmBool(Issteel),
                    isFalse = FsmEvent.Finished
                });
                Log("Change HUD");
            }
        }
    }
}
