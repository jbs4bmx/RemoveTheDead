using Comfort.Common;
using EFT;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace RemoveTheDead.Helpers
{
    public class Service_Remover : MonoBehaviour
    {
        float Timer { get; set; }
        private bool isRemoving = false;

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
                StaticManager.Instance.StartCoroutine(QueueRemoval());
                Timer = 0f;
            }
        }

        // Run dead bot removals on an interval as specified by the player.
        IEnumerator QueueRemoval()
        {
            if (isRemoving) yield break;
            isRemoving = true;
            yield return new WaitForSeconds(RTDPlugin.TimeToClean.Value * 60f);
            yield return new WaitForSeconds(3f);
            foreach (BotOwner bot in FindObjectsOfType<BotOwner>())
            {
                if (!bot.HealthController.IsAlive && Vector3.Distance(Myplayer.Transform.position, bot.Transform.position) >= RTDPlugin.DistToClean.Value)
                {
                    RemoveBotAndWeapons(bot);
                }
            }
            isRemoving = false;
        }

        // Manual removal (shortcut or button)
        public void RunRemovalNow()
        {
            foreach (BotOwner bot in FindObjectsOfType<BotOwner>())
            {
                if (!bot.HealthController.IsAlive &&
                    Vector3.Distance(Myplayer.Transform.position, bot.Transform.position) >= RTDPlugin.DistToClean.Value)
                {
                    RemoveBotAndWeapons(bot);
                }
            }
        }

        // Run removal of inactive bot weapons and then the inactive bots themselves
        private void RemoveBotAndWeapons(BotOwner bot)
        {
            // Remove the bot's GameObject
            bot.gameObject.SetActive(false);
            // Cleanup any loose weapons around the bot
            RemoveNearbyWeapons(bot.Transform.position, 3.0f);
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
        Player Myplayer => Gameworld.AllAlivePlayersList[0];
        GameWorld Gameworld => Singleton<GameWorld>.Instance;
    }
}
