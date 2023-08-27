/*
    HeresyDebug -- A Mod for Blasphemous 2
    Copyright (C) 2023  vivienne (vivipatching)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppSystem.Linq;
using TGK.Framework.Quest;
using TGK.Game.Managers;

namespace HeresyDebug;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
}

[HarmonyPatch(typeof(QuestManager))]
[HarmonyPatch("GetQuestVariable")]
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
                Il2CppSystem.Console.WriteLine("[YermaSvsonaSusonaSoftlockFix!] Applied! Hopefully progression works now??");
            }
        }
    }
}
