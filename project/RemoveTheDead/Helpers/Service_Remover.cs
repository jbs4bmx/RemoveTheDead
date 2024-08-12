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


        async void QueueRemoval()
        {
            await Task.Delay(10000);
            foreach (BotOwner bot in FindObjectsOfType<BotOwner>())
            {
                if (!bot.HealthController.IsAlive && Vector3.Distance(Myplayer.Transform.position, bot.Transform.position) >= RTDPlugin.DistToClean.Value)
                {
                    bot.gameObject.SetActive(false);
                }
            }
        }

        public void RunRemovalNow(bool forceRemove = false)
        {
            if (forceRemove)
            {
                foreach (BotOwner bot in FindObjectsOfType<BotOwner>())
                {
                    if (!bot.HealthController.IsAlive && Vector3.Distance(Myplayer.Transform.position, bot.Transform.position) >= RTDPlugin.DistToClean.Value)
                    {
                        bot.gameObject.SetActive(false);
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
