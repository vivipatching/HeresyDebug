using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppSystem.Linq;
using TGK.Framework.Quest;
using TGK.Game.Managers;

// HeresyDebug
// Author: vivienne (vivipatching)
// License: GPL-3

namespace HeresyDebug;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

        harmony.PatchAll();
        // Plugin startup logic
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
}

[HarmonyPatch(typeof(QuestManager))]
[HarmonyPatch("GetQuestVariable")] // if possible use nameof() here
[HarmonyPatch(new System.Type[] { typeof(int), typeof(int), typeof(string) })]
class Patch01
{
    static void Postfix(ref QuestManager __instance, ref QuestVariable __result, ref int questId, ref int varId, ref string labelToShowErrorIfNeed)
    {
        var rawdata = __instance.GetQuestData(questId, "[YermaSvsonaSusonaSoftlockFix!] it broke :(");
        foreach (var questvar in rawdata.GetAllVars().ToList())
        {
            if (rawdata.Name == "ST23" && questvar.id == "BAD_ENDING" && questvar.IntId == varId)
            {
                questvar.currentValue = 0;
                __result.currentValue = 0;
            }
            Il2CppSystem.Console.WriteLine("[YermaSvsonaSusonaSoftlockFix!] Applied! Hopefully progression works now??");
        }
    }
}
