using HarmonyLib;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace NoPlanetaryScrapPrice
{
    [HarmonyPatch]
    public class RemovePrice()
    {
        // Patch triggers on terminal moving to new page
        [HarmonyPatch(typeof(Terminal), nameof(Terminal.LoadNewNode))]
        [HarmonyPostfix]
        public static void removeTerminalNodePrice(Terminal __instance, TerminalNode node)
        {
            // Initiate logging for this method
            var _logger = BepInEx.Logging.Logger.CreateLogSource("Kyle_Plugin");
            _logger.LogInfo($"Node: {node.name}");

            // List of terminal nodes that we want to edit
            string[] moonNames = ["85route", "85routeConfirm", "7route", "7routeConfirm", "8route", "8routeConfirm"];

            // On MoonsCatalogue node loading trigger if block
            if (node.name == "MoonsCatalogue")
            {
                _logger.LogInfo("Moons node detected");

                // Find all ScriptableObjects of type TerminalNode and filter based on moonNames list
                UnityEngine.Object[] nodeList = Resources.FindObjectsOfTypeAll<TerminalNode>();
                IEnumerable<Object> filteredNodes = nodeList.Where(y=>moonNames.Any(z=>z.Equals(y.name)));

                // Change itemCost of filteredNodes to 0
                foreach (Object obj in filteredNodes)
                {
                    TerminalNode terminalNode = obj as TerminalNode;
                    terminalNode.itemCost = 0;
                }

                /* foreach (UnityEngine.Object obj in nodeList) 
                { 
                    _logger.LogInfo($"{obj.name}"); 
                    if (obj.name == "85routeConfirm")
                    {
                        TerminalNode terminalNode = obj as TerminalNode;
                        if (terminalNode == null)
                        {
                            _logger.LogError("Can't find terminal node");
                        }
                        else
                        {
                            _logger.LogError(terminalNode.GetType());
                            terminalNode.itemCost = 0;
                        }
                    }
                }
                */
            }
        }
    }
}
