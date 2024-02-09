using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Content;
using SpooninDrawer.Engine.Objects;



namespace SpooninDrawer.Objects.Text
{
    public class TestText : BaseTextObject
    {
        public TestText(SpriteFont font)
        {
            _font = font;
            Text = RStrings.GoodLuckPlayer1;
            _color = Color.Black;
        }
            

    }
}
