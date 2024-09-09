using Exiled.API.Features;
using Exiled.CustomItems.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xui
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Name => "gay";
        public override string Author => base.Author;
        public override void OnEnabled()
        {
            new Dufalak().Register();
            base.OnEnabled();
        }
    }
}
