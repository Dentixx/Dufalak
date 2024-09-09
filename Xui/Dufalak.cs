using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using Exiled.API.Features;
using Exiled.API.Features.Roles;

namespace Xui
{
    [CustomItem(ItemType.Painkillers)]
    public sealed class Dufalak : CustomItem
    {
        public override uint Id { get; set; } = 0;
        public override string Name { get; set; } = "Dufalak";
        public override string Description { get; set; } = "ponos";
        public override float Weight { get; set; } = 0.1f;
        public override SpawnProperties SpawnProperties { get; set; }
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsedItem += DufalakUsed;
            Exiled.Events.Handlers.Player.Spawned += GiveItem;
        }
        public void DufalakUsed(UsedItemEventArgs eventArgs)
        {
            if (Check(eventArgs.Item))
            {
                Timing.CallDelayed(60f, () =>
            {
                Exiled.API.Features.Map.PlaceTantrum(eventArgs.Player.Position);
            });
            }
            Timing.RunCoroutine(Timer(eventArgs.Player));
        }
        public IEnumerator<float> Timer(Player player)
        {
            for (int i = 60; i >= 0; i--)
            {
                player.Broadcast(1, $"До облегчения {i} секунд");
                yield return Timing.WaitForSeconds(1);
            }
        }
        public static void GiveItem(SpawnedEventArgs eventArgs)
        {
            if (eventArgs.Player.Role.Type == PlayerRoles.RoleTypeId.ClassD)
            {
                CustomItem.TryGive(eventArgs.Player, 0);
            }
            Exiled.API.Features.Log.Info(1);
        }
        public override ItemType Type { get => _type; set => _type = value; }
        private ItemType _type = ItemType.Painkillers;
    }
}
