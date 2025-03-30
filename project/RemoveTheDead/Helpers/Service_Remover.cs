using Comfort.Common;
using EFT;
using System.Threading.Tasks;
using UnityEngine;

namespace RemoveTheDead.Helpers
{
    public class Service_Remover : MonoBehaviour
    {
        float Timer {  get; set; }
        void Update()
        {
            if (!Ready())
            {
                Timer = 0f;
                return;
            }

            if (RTDPlugin.EnableClean.Value)
            {
                Timer += Time.deltaTime;
            }
            if (Timer >= RTDPlugin.TimeToClean.Value)
            {
                QueueRemoval();
                Timer = 0f;
            }
        }

        // Run dead bot removals on an interval as specified by the player.
        async void QueueRemoval()
        {
            await Task.Delay(10000);
            foreach (BotOwner bot in FindObjectsOfType<BotOwner>())
            {
                if (!bot.HealthController.IsAlive && Vector3.Distance(Myplayer.Transform.position, bot.Transform.position) >= RTDPlugin.DistToClean.Value)
                {
                    RemoveBotAndWeapons(bot);
                }
            }
        }

        // Run dead bot removals instantly when player presses the button.
        public void RunRemovalNow(bool forceRemove = false)
        {
            if (forceRemove)
            {
                foreach (BotOwner bot in FindObjectsOfType<BotOwner>())
                {
                    if (!bot.HealthController.IsAlive && Vector3.Distance(Myplayer.Transform.position, bot.Transform.position) >= RTDPlugin.DistToClean.Value)
                    {
                        RemoveBotAndWeapons(bot);
                    }
                }
            }
        }

        // Run removal of inactive bot weapons and then the inactive bots themselves
        private void RemoveBotAndWeapons(BotOwner bot)
        {
            // Remove the bot's GameObject
            bot.gameObject.SetActive(false);

            // Cleanup any loose weapons around the bot
            RemoveNearbyWeapons(bot.Transform.position, 2.0f); // Adjust radius as needed
        }

        // Removes loose weapons around a specific position
        private void RemoveNearbyWeapons(Vector3 position, float radius)
        {
            // Find all GameObjects in the scene
            foreach (var obj in FindObjectsOfType<GameObject>())
            {
                if (obj == null) continue;

                // Check if the object is a world item and a weapon
                var itemView = obj.GetComponent<EFT.Interactive.LootItem>();
                if (itemView != null && itemView.Item is EFT.InventoryLogic.Weapon)
                {
                    // Check if it's within range of the removed bot
                    if (Vector3.Distance(position, obj.transform.position) <= radius)
                    {
                        Destroy(obj);
                    }
                }
            }
        }

        public bool Ready() => Gameworld != null && Gameworld.AllAlivePlayersList != null && Gameworld.AllAlivePlayersList.Count > 0 && !(Myplayer is HideoutPlayer);

        Player Myplayer
        { get => Gameworld.AllAlivePlayersList[0]; }

        GameWorld Gameworld
        { get => Singleton<GameWorld>.Instance; }
    }
}
