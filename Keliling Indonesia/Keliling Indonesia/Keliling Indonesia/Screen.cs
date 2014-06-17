using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Keliling_Indonesia
{
    class Screen
    {
        protected EventHandler screenEvent;
        public Screen(EventHandler screenEvent)
        {
            this.screenEvent = screenEvent;
        }

        public virtual void Update(GameTime time)
        {
        }

        public virtual void Draw(SpriteBatch batch)
        {
        }
    }
}
