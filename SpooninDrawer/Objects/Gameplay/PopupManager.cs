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
        public InteractablePopupBox MinigamePopupBox;
        public InteractablePopupBox ControlDisplayBox;
        public InteractablePopupBox RollCreditsBox;
        public DialogBox DialogBox;
        private Texture2D InteractableTexture;
        private Texture2D AddInventoryTexture;
        private Texture2D MinigameTexture;
        private Texture2D ControlDisplayTexture;
        private Texture2D RollCreditsTexture;

        private List<InteractablePopupBox> AddInventoryPopupBoxes;
        private SpriteFont Font;
        private Vector2 PlayerPosition;
        private Vector2 RollCreditPositionHolder;

        private int DialogContinuingTexts = 0;

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

            DialogBox = new DialogBox(new GameplayText(Font, "DialogBox"), new GameplayText(Font, "Name"), new GameplayText(Font, "subtitle"), new Vector2(camera.BoundingRectangle.Width / 2, camera.Position.Y), resolution);
            DialogBox.GameplayText.zIndex = 23;
            DialogBox.zIndex = 22;
            DialogBox.Deactivate();


        }
        public void SetPopupBoxTextures(Texture2D interactable, Texture2D addInventory, Texture2D minigame, Texture2D controldisplay, Texture2D rollCredits)
        {
            InteractableTexture = interactable;
            AddInventoryTexture = addInventory;
            MinigameTexture = minigame;
            ControlDisplayTexture = controldisplay;
            RollCreditsTexture = rollCredits;
        }
        public void LoadMinigameBox()
        {
            MinigamePopupBox = new InteractablePopupBox(new GameplayText(Font, "Put the spoon in the drawer."), new Vector2(0, 0), MinigameTexture);
            MinigamePopupBox.GameplayText.zIndex = 20;
            MinigamePopupBox.zIndex = 19;
            MinigamePopupBox.Deactivate();
        }
        public void LoadRollCreditsBox()
        {
            RollCreditsBox = new InteractablePopupBox(new RollCreditsText(Font, "Thank you for playing!"), new Vector2(0, 0), RollCreditsTexture);
            RollCreditsBox.GameplayText.zIndex = 30;
            RollCreditsBox.zIndex = 29;
            RollCreditsBox.Deactivate();
        }
        public void LoadControlDisplayBox(string text)
        {
            ControlDisplayBox = new InteractablePopupBox(new GameplayText(Font, text), PlayerPosition + new Vector2(0, 0), ControlDisplayTexture);
            ControlDisplayBox.GameplayText.zIndex = 11;
            ControlDisplayBox.zIndex = 10;
            ControlDisplayBox.Activate();
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
        public void ActivateMinigameBox(string text, Vector2 position)
        {
            MinigamePopupBox.Position = position;
            ChangeDialogDisplayTextSpeedNormal();
            MinigamePopupBox.ChangeText(text);
            MinigamePopupBox.Activate();
        }
        public void ActivateRollCreditsBox(string text, Vector2 position)
        {
            RollCreditsBox.Position = position;
            ChangeDialogDisplayTextSpeedNormal();
            RollCreditsBox.ChangeText(text, 1000);
            RollCreditsBox.GameplayText.SetTransparency(0.0f);
            RollCreditsBox.Activate();
        }
        public void ActivateInteractPopupBox(Vector2 playerPosition)
        {
            InteractableItemPopupBox.Position = playerPosition + new Vector2(100, 0);
            InteractableItemPopupBox.Activate();
        }
        public void DeactivateDialogBox()
        {
            DialogBox.ResetDialogBox();
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
                if (popupBox != null)
                {
                    popupBox.Activate(ItemName);
                }
                else
                {
                    popupBox = AddInventoryPopupBoxes.First();
                    int MovePopupBoxUp = -50 * (AddInventoryPopupBoxes.Count - 1);
                    foreach (var item in AddInventoryPopupBoxes)
                    {
                        if (popupBox.PopupTime > item.PopupTime)
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
            if (ControlDisplayBox.Active)
            {
                ControlDisplayBox.Position = new Vector2(camera.Position.X, camera.Center.Y + (camera.Center.Y - camera.Position.Y) - ControlDisplayBox.Height);
            }
            if (RollCreditsBox.Active)
            {
                if (RollCreditsBox.GameplayText.GetTransparency() < 1.0f)
                {
                    RollCreditsBox.GameplayText.TransparencyUp();
                    RollCreditPositionHolder = camera.Center;
                }
                else
                {
                    RollCreditPositionHolder.Y--;
                    RollCreditsBox.Position = RollCreditPositionHolder;
                }
            }
        }
    }
}