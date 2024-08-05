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
        private List<InteractablePopupBox> AddInventoryPopupBoxes;
        private SpriteFont Font;
        private Vector2 PlayerPosition;



        public PopupManager(SpriteFont font, Vector2 playerPosition, OrthographicCamera camera)
        {
            Font = font;
            PlayerPosition = playerPosition;
            AddInventoryPopupBoxes = new List<InteractablePopupBox>();

            InteractableItemPopupBox = new InteractablePopupBox(new GameplayText(Font, "Interact"), PlayerPosition + new Vector2(100, 100));
            InteractableItemPopupBox.GameplayText.zIndex = 13;
            InteractableItemPopupBox.zIndex = 12;
            InteractableItemPopupBox.Deactivate();
            
            AddInventoryPopupBox = new InteractablePopupBox(new GameplayText(Font, "Test"), new Vector2(camera.BoundingRectangle.Width / 2, camera.Position.Y));
            AddInventoryPopupBox.GameplayText.zIndex = 13;
            AddInventoryPopupBox.zIndex = 12;
            AddInventoryPopupBox.Deactivate();

        }
        private void LoadPopupBox(InteractablePopupBox popupBox)
        {
            /*doesnt work
            popupBox = new InteractablePopupBox(new GameplayText(Font, "Holder"), PlayerPosition + new Vector2(100, 100));
            popupBox.GameplayText.zIndex = 13;
            popupBox.zIndex = 12;
            popupBox.Deactivate();*/
        }
        public void ActivateAddInventoryPopupBox(string ItemName, GameTime gameTime)
        {            
            AddInventoryPopupBox.FadeAwayPopup = true;
            AddInventoryPopupBox.PopupTime = gameTime.TotalGameTime.TotalSeconds;
            AddInventoryPopupBox.Activate(ItemName);
        }
        public void Update(GameTime gameTime, OrthographicCamera camera)
        {
            if(AddInventoryPopupBox.Active)
                AddInventoryPopupBox.Position = new Vector2(camera.Position.X, camera.Center.Y);

            if (AddInventoryPopupBox.FadeAwayPopup & gameTime.TotalGameTime.TotalSeconds > AddInventoryPopupBox.PopupTime + AddInventoryPopupBox.FadeAwayTime)
            {
                AddInventoryPopupBox.Deactivate();
            }
        }
    }
}