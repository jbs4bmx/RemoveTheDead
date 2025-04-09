using BepInEx;
using BepInEx.Configuration;
using RemoveTheDead.Helpers;
using RemoveTheDead.Utilities;
using System;
using UnityEngine;
using static RemoveTheDead.Utilities.VersionChecker;

namespace RemoveTheDead
{
    [BepInPlugin("com.jbs4bmx.RemoveTheDead","RTD","311.3.1")]
    [BepInDependency("com.SPT.core", "3.11.0")]
    public class RTDPlugin : BaseUnityPlugin
    {
        public const int TarkovVersion = 35392;

        internal static Service_Remover Script;
        internal static GameObject Hook;

        internal static ConfigEntry<bool> EnableClean;
        internal static ConfigEntry<float> TimeToClean;
        internal static ConfigEntry<int> DistToClean;
        internal static ConfigEntry<string> RTDButton { get; set; }
        internal static ConfigEntry<KeyboardShortcut> RTDShortcut;

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
                15f,
                new ConfigDescription
                (
                    "Time (in minutes) to remove bodies.",
                    new AcceptableValueRange<float>(1f, 60f),
                    new ConfigurationManagerAttributes
                    {
                        IsAdvanced = false,
                        ShowRangeAsPercent = false,
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
            RTDShortcut = Config.Bind(
                sectionName,
                "Remove all dead bodies now",
                new KeyboardShortcut(KeyCode.F5),
                new ConfigDescription(
                    "The keyboard shortcut that runs the body removal service instantly.",
                    null,
                    new ConfigurationManagerAttributes
                    {
                        Order = 0
                    }
                )
            );

            Hook = new GameObject("IR Object");
            Script = Hook.AddComponent<Service_Remover>();
            DontDestroyOnLoad(Hook);
        }

        public void RTDButtonActionDrawer(ConfigEntryBase entry)
        {
            bool button = GUILayout.Button("Remove The Dead", GUILayout.ExpandWidth(true));
            if (button)
            {
                var runner = new Service_Remover();
                runner.RunRemovalNow();
            }
        }

        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(RTDShortcut.Value.MainKey))
            {
                var runner = new Service_Remover();
                runner.RunRemovalNow();
            }
        }
    }
}
