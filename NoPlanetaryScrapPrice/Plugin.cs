using BepInEx;
using HarmonyLib;

namespace NoPlanetaryScrapPrice
{
    // Unique variables for BepInEx to use
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        // On game startup trigger function
        private void Awake()
        {
            // Plugin Loaded
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            // Create new harmony patcher for this plugin
            Harmony patcher = new(PluginInfo.PLUGIN_GUID);
            Logger.LogInfo($"Enabled, patching now");

            // Include the RemovePrice patch
            patcher.PatchAll(typeof(RemovePrice));
        }
    }
}