using BepInEx;
using BepInEx.Configuration;
using RemoveTheDead.Helpers;
using RemoveTheDead.Utilties;
using System;
using UnityEngine;
using static RemoveTheDead.Utilties.VersionChecker;

namespace RemoveTheDead
{
    [BepInPlugin("com.jbs4bmx.RemoveTheDead","RTD","380.0.1")]
    public class RTDPlugin : BaseUnityPlugin
    {
        public const int TarkovVersion = 29197;

        internal static GameObject Hook;

        internal static ConfigEntry<bool> DropItems;
        internal static ConfigEntry<bool> EnableClean;
        internal static ConfigEntry<float> TimeToClean;
        internal static ConfigEntry<int> DistToClean;
        internal static ConfigEntry<float> DropItemsChance;
        public static ConfigEntry<string> RTDButton { get; set; }

        void Awake()
        {
            const string sectionName = "Dead Body Removal Service";

            // Verify EFT version is correct.
            if (!VersionChecker.CheckEftVersion(Logger, Info, Config))
            {
                throw new Exception("Invalid EFT Version");
            }

            // Build UI and establish actionable parameters
            EnableClean = Config.Bind(
                sectionName,
                "Enable Auto Removal.",
                true,
                new ConfigDescription
                (
                    "Enable Auto Removal?",
                    null,
                    new ConfigurationManagerAttributes
                    {
                        Order = 4,
                    }
                )
            );
            TimeToClean = Config.Bind(
                sectionName,
                "Time to remove",
                120f,
                new ConfigDescription
                (
                    "Time to remove bodies.",
                    null,
                    new ConfigurationManagerAttributes
                    {
                        Order = 3,
                    }
                )
            );
            DistToClean = Config.Bind(
                sectionName,
                "Distance to Clean.",
                30,
                new ConfigDescription
                (
                    "How far away should bodies be for cleanup.",
                    null,
                    new ConfigurationManagerAttributes
                    {
                        Order = 2,
                    }
                )
            );
            RTDButton = Config.Bind(
                sectionName,
                "Remove The Dead",
                "Remove all dead bodies now",
                new ConfigDescription(
                    "Remove all dead bodies now",
                    null,
                    new ConfigurationManagerAttributes
                    {
                        Order = 1,
                        CustomDrawer = RTDButtonActionDrawer
                    }
                )
            );

            Hook = new GameObject("IR Object");
            DontDestroyOnLoad(Hook);

            
        }
        public void RTDButtonActionDrawer(ConfigEntryBase entry)
        {
            bool button = GUILayout.Button("Remove The Dead", GUILayout.ExpandWidth(true));
            if (button)
            {
                var runner = new Service_Remover();
                runner.RunRemovalNow(true);
            }
        }
    }
}
