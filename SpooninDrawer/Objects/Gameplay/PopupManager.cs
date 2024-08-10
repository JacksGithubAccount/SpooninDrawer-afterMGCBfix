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
using SpooninDrawer.Engine.Objects;

namespace SpooninDrawer.Objects.Gameplay
{
    public class PopupManager
    {
        public InteractablePopupBox InteractableItemPopupBox;
        public InteractablePopupBox AddInventoryPopupBox;
        private Texture2D InteractableTexture;
        private Texture2D AddInventoryTexture;

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
        public void SetPopupBoxTextures(Texture2D interactable, Texture2D addInventory)
        {
            InteractableTexture = interactable;
            AddInventoryTexture = addInventory;
        }
        private InteractablePopupBox CreatePopupBox(string ItemName)
        {
            InteractablePopupBox popupBox = new InteractablePopupBox(new GameplayText(Font, "Holder"), new Vector2(100, 100), AddInventoryTexture);
            popupBox.GameplayText.zIndex = 13;
            popupBox.zIndex = 12;
            popupBox.Deactivate();
            return popupBox;
        }
        public InteractablePopupBox ActivateAddInventoryPopupBox(string ItemName, GameTime gameTime)
        {
            if (AddInventoryPopupBoxes.Count < 10)
            {
                InteractablePopupBox popupBox = CreatePopupBox(ItemName);
                popupBox.PopupTime = gameTime.TotalGameTime.TotalSeconds;
                popupBox.Activate(ItemName, true);
                AddInventoryPopupBoxes.Add(popupBox);
                return popupBox;
            }
            else
            {
                InteractablePopupBox popupBox = AddInventoryPopupBoxes.Find((x => x.Active == false));
                if(popupBox != null)
                {
                    popupBox.Activate(ItemName);
                }
                else
                {
                    popupBox = AddInventoryPopupBoxes.First();
                    foreach (var item in AddInventoryPopupBoxes)
                    {
                        if(popupBox.PopupTime > item.PopupTime)
                        {
                            popupBox = item;
                        }
                    }
                }
                return popupBox;
            }
            
        }
        public void DeactivateAddInventoryPopupBox(InteractablePopupBox popupBox)
        {
            popupBox.Deactivate();
            AddInventoryPopupBoxes.Remove(popupBox);
        }
        public void Update(GameTime gameTime, OrthographicCamera camera)
        {
            foreach (InteractablePopupBox popupBox in AddInventoryPopupBoxes)
            {
                if (popupBox.Active)
                {
                    popupBox.Position = new Vector2(camera.Position.X, camera.Center.Y);

                    if (popupBox.FadeAwayPopup & gameTime.TotalGameTime.TotalSeconds > popupBox.PopupTime + popupBox.FadeAwayTime)
                    {
                        DeactivateAddInventoryPopupBox(popupBox);
                        break;
                    }
                }
            }
        }
    }
}