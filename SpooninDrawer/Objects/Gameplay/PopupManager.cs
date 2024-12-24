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
using static System.Net.Mime.MediaTypeNames;

namespace SpooninDrawer.Objects.Gameplay
{
    public class PopupManager
    {
        public InteractablePopupBox InteractableItemPopupBox;
        public InteractablePopupBox AddInventoryPopupBox;
        public DialogBox DialogBox;
        private Texture2D InteractableTexture;
        private Texture2D AddInventoryTexture;

        private List<InteractablePopupBox> AddInventoryPopupBoxes;
        private SpriteFont Font;
        private Vector2 PlayerPosition;


        public PopupManager(SpriteFont font, Vector2 playerPosition, OrthographicCamera camera, Resolution resolution)
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

            DialogBox = new DialogBox(new GameplayText(Font, "Interact"), new Vector2(camera.BoundingRectangle.Width / 2, camera.Position.Y), resolution);
            DialogBox.GameplayText.zIndex = 13;
            DialogBox.zIndex = 12;
            DialogBox.Deactivate();

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
        public void ActivateDialogBox(string text)
        {
            ChangeDialogDisplayTextSpeedNormal();
            DialogBox.ChangeText(text);
            DialogBox.Activate();
        }
        public void DeactivateDialogBox()
        {
            DialogBox.ChangeText("");
            DialogBox.Deactivate();            
        }
        public void ChangeDialogDisplayTextSpeedFast()
        {
            DialogBox.ChangeDisplayTextSpeed(DialogBox.DisplaySpeedFast);
        }
        public void ChangeDialogDisplayTextSpeedNormal()
        {
            DialogBox.ChangeDisplayTextSpeed(DialogBox.DisplaySpeedNormal);
        }
        public void ChangeDialogDisplayTextSpeedSlow()
        {
            DialogBox.ChangeDisplayTextSpeed(DialogBox.DisplaySpeedSlow);
        }

        public InteractablePopupBox ActivateAddInventoryPopupBox(string ItemName, GameTime gameTime)
        {
            if (AddInventoryPopupBoxes.Count < 10)
            {
                InteractablePopupBox popupBox = CreatePopupBox(ItemName);
                popupBox.PopupTime = gameTime.TotalGameTime.TotalSeconds;
                popupBox.Activate(ItemName, true);
                AddInventoryPopupBoxes.Add(popupBox);
                if (AddInventoryPopupBoxes.Count > 1)
                {
                    int MovePopupBoxUp = -50 * (AddInventoryPopupBoxes.Count - 1);
                    foreach (var item in AddInventoryPopupBoxes)
                    {
                        item.MovePopupBoxUp = new Vector2(0, MovePopupBoxUp);
                        MovePopupBoxUp = MovePopupBoxUp + 50;
                    }
                    }
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
                    int MovePopupBoxUp = -50 * (AddInventoryPopupBoxes.Count - 1);
                    foreach (var item in AddInventoryPopupBoxes)
                    {
                        if(popupBox.PopupTime > item.PopupTime)
                        {
                            popupBox = item;
                            AddInventoryPopupBoxes.Remove(item);
                            AddInventoryPopupBoxes.Add(popupBox);
                        }                        
                        item.MovePopupBoxUp = new Vector2(0, MovePopupBoxUp);
                        MovePopupBoxUp = MovePopupBoxUp + 50;
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
            if (DialogBox.Active)
            {
                DialogBox.Position = new Vector2(camera.Position.X, camera.Center.Y + (camera.Center.Y - camera.Position.Y) - DialogBox.Height);
                DialogBox.Update();
            }
        }
    }
}