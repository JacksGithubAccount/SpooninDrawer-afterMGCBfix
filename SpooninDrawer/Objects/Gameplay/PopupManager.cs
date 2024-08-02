using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SpooninDrawer.Objects.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using MonoGame.Extended;

namespace SpooninDrawer.Objects.Gameplay
{
    public class PopupManager
    {
        public InteractablePopupBox InteractableItemPopupBox;
        public InteractablePopupBox AddInventoryPopupBox;
        private SpriteFont Font;
        private Vector2 PlayerPosition;
        private GameTime gameTime;



        public PopupManager(SpriteFont font, Vector2 playerPosition, OrthographicCamera camera)
        {
            Font = font;
            PlayerPosition = playerPosition;
            
            InteractableItemPopupBox = new InteractablePopupBox(new GameplayText(Font, "Interact"), PlayerPosition + new Vector2(100, 100));
            InteractableItemPopupBox.GameplayText.zIndex = 13;
            InteractableItemPopupBox.zIndex = 12;
            InteractableItemPopupBox.Deactivate();
            
            AddInventoryPopupBox = new InteractablePopupBox(new GameplayText(Font, "Test"), new Vector2(camera.Position.X, camera.Position.Y));
            AddInventoryPopupBox.GameplayText.zIndex = 13;
            AddInventoryPopupBox.zIndex = 12;
            AddInventoryPopupBox.Deactivate();


        }
        private void LoadPopupBox(InteractablePopupBox popupBox)
        {
            //doesnt work
            popupBox = new InteractablePopupBox(new GameplayText(Font, "Holder"), PlayerPosition + new Vector2(100, 100));
            popupBox.GameplayText.zIndex = 13;
            popupBox.zIndex = 12;
            popupBox.Deactivate();
        }
        public void Update(GameTime gameTime, OrthographicCamera camera)
        {
            AddInventoryPopupBox.Position = new Vector2(camera.Position.X, camera.Position.Y);
        }
    }
}