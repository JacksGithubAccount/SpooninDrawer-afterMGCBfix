using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer
{
    internal class notes
    {
        /*8/22/2023
         * -the start
         * 8/27/2023
         * 9/4/2023
         * -Added basic engine stuff to display titlescreen
         * 9/5/2023
         * -Added input engine stuff
         * 9/9/2023
         * -added more engine stuff
         * 9/11/2023
         * -added all but gameplay state
         * 9/13/2023
         * -added game play state, done ch10
         * 9/16/2023
         * -added input for moving up and down, no animation yet for player
         * 9/19/2023
         * -started adding idle animation, also up an ddown doesn't work
         * -Idle animation works, also added method to make an animation loop
         * -up and down works now
         * 9/20/2023
         * -updated title screen to fit intended resolution for now
         * -added menu arrow and got arrow to move around title screen, also make arrow loop back around when going past first and last option
         * -trying to polymorph screens; solved using an interface for base screen
         * 9/21/2023
         * -need to make enter key context sensitive on title screen
         * 9/22/2023
         * -added start settings and exit, also a back button, no need for load yet
         * -will need input keys, maybe? unsure, future consideration
         * 9/23/2023
         * -maybe time to start on map?
         * 9/25/2023
         * -rmoved a null check in screen and added empty screen to load in place of null
         * -added FPS checker
         * -Spritefont issue where nothing would print, turns out the <end> variable in the spritefont needs to be 126 to allow all characters
         * 9/26/2023
         * -Added way to change FPS and player movement is now the same regardless of FPS.
         * 9/28/2023
         * -Added object pool
         * 9/30/2023
         * -added pipeline extension as a separate project then added through content pipeline
         * 10/1/2023
         * -doing same as last time, but for animations
         * -somehow broke BaseGameObject and I had to add an empty constructor to it, now the title screen doesn't load.
         * -implementation of object pool broke it, the render method activate bool check in base game object is doing it
         * 10/7/2023
         * -XML importing is not working. I can't seem to figure it out. Will skip for now.
         * -Got screen to work, instead of creating the splash screen in the add game object method, instantiated it as own varible and activated it
         * -arrow disappears on changing menu screens, the new screen is placed on top of the arrow
         * -added several methods to help with the object pool integration, but now all screens that deactivate don't get activated
         * 10/9/2023
         * -Is there a need to disable? Only one screen gets added to the list, so just make that the top one. right? 
         * -Doesn't work. top screen is still drawn to the top
         * -Fixed, all I had to do was use the zIndex to make the screen render on top of the other one.
         * 10/10/2023
         * -tryin XML stuff again, same issue
         * 10/23/2023
         * -Figured out the XML problem. Book I was using has <Asset Type="Engine2D.PipelineExtensions:AnimationData"> which could not resolve
         * -rb whitaker's site showed <Asset Type="MonoGameXmlContent.Weapon"> for their example. Changing : to . allowed it to build
         * 10/27/2023
         * -Changed right, left and idle animation data to XML files
         * -added a resource file to hold string values and added a way to translate/switch between same named resource
         * 10/30/2023
         * 11/7/23
         * -installed monogame extended
         * 11/14/23
         * -started on testing camera
         * 11/15/23
         * -Got a camera, and centered it on player. The camera only exists in test camera state/
         * -started on level editor, ran into issue where the overrides aren't working on forms, had to change target framework to 4.8
         * -properties wouldn't let me do that, had to edit csproj and change target framework to net48 to make it work
         * 11/19/23
         * -added more stuff to gameeditor, but lots of stuff not working. Says it's not in the Monogame framework? Will need to figure out
         * 11/21/23
         * -entered a possible fix to the graphics device not detected. need to test if it works.
         * 12/19/23
         * -forms isnt working, cant get it to work. installed tiled instead to try that. need to test it.
         * 12/23/23
         * -tiled doesn't work. latest version of extended(3.8.0) removed a reference that tiled uses to build it's map.
         * -current solution is to install an alpha pre-release version of the newest update(3.8.1), which worked.
         * -added test map through tiled
         * -need to move camera from test state to actual state.
         * 12/26/23
         * -there's an issue with spritebatch begins where you need the transformation matrixc of the camera to get it to work but the begins is called in a different class
         * 12/27/23
         * -added a method to retrieve the matrix and passed it to the main game, now the camera moves with the character
         * -new issue now where the map also moves with the character
         * -fixed, had to add the camera view matrix into the draw method for the tiled map
         * 12/29/23
         * -tiled map transparency isn't working. they render as black isntead
         * -adding GraphicsDevice.BlendState = BlendState.AlphaBlend; to the draw method made it work
         * -added pause, it works, but input doesn't work correctly. may need to remove current input control to add one that works better
         * -added a version of input control, needs further work. Turns out I already created a version of this in splash state, imported over to gameplay state
         * -release input doesn't work, will need to lookfurther into it, also add more that'll let you change keys ingame
         * -found out why release wasn't working. I was leaving in the part of the code where it was checking if key was pressed. Release works now
         * 12/30/23
         * -appemted to intgrate control change from prevous project. don't think it's working. may have to inport it instead
         * 1/2/24
         * -had to remove changes and ended up importing the inputter and action key from old project and it works
         * 1/3/24
         * -next step is adding collision to map, researching on tiled at the moment
         * 1/8/24
         * -installed TiledCs and TiledSharp, one or the other is needed to make coding for Tiled maps easier
         * 1/9/2024
         * -more work trying to get TiledSharp to display tiled map following https://www.youtube.com/watch?v=RzTaLFHBu88&t
         * 1/10/2024
         * -made maps display, now to work on collision. video above had code for getting the collision for map. Now to add palyer collision and interaction
         * 1/21/2024
         * -AABB collision only takes basegame object, two ways I see to proceed, either make new collision for rect or turn map rect to new object that inherits base game object
         *1/22/2024
         *-turned into basegame object, but the collision isn't working, will need more test
         *1/23/2024
         *-turns out I forgot to add the boundingbox, it now works. Now need to turn off the speed reduction
         *1/24/2024
         *-since direction is already booled, just need to find way to make it so you can't move in already moving directions, but other directions are fine
         *1/30/2024
         *-attempting movedirection detecting tiles that will collide with
         *1/31/2024
         *-adding movedirection works for collision, but lost the ability to move diagonally
         *-fixed by removing movedirection, but keeping the bools for stoping direction on collection, however, can move too into collidable object
         *2/1/2024
         *-added position readjustment if inside a collidable map tile, it's not perfect, but it's less worse
         *-the player actually has two bounding boxes. Need to fogure out which is colliding and reset position based on that
         *2/2/2024
         *-colliding up or down and moving left or right makes player go through collidable object, is probably related to position not matching up with the bounding boxes
         *2/3/2024
         *-added boxes to look ahead in moving direction to find collision. Also MGCB broke, can't build anything amymore.
         *2/4/2024
         *-MGCB broke, after must troubleshooting, conclusion is problem with this program. Maybe. More troubleshoot may be needed.
         *2/7/2024
         *-Finally fixed it. After much troubleshootin, the fix was there was something wrong with the tiled map that I was using. Created new map works.
         *-added rectangle to look up, down is still missing. However, the collision is still not working correctly
         *2/8/2024
         *-added down box to look ahead. added way to turn off the collide bools. It's not perfect but, it works.
         *2/9/2024
         *-shortened look ahead boxes so now the boxes don't intersect with collision. 
         *-added walls, now you become uncollidable going left or right when colliding up.
         *2/12/2024
         *-drew the look ahead rects, the up and left don't look right, will need to fix.
         *2/13/2024
         *-added texture to box and changed color
         *2/15/2024
         *-The problem seems to be that only one instance of the collider exists and that's what gets passed on
         *2/18/2024
         *-added a list that adds the maptiles that are collided with, but it doesn't seem to work.
         *-changed the bools around since return breaks which could be the cause of of the issue. but now collision only works with bottom wall
         *2/19/2024
         *-adjusted the collision detector to accept a second method for when no collision, removing need for bools. But now back at the same issue where can uncolide with different objects if colliding
         *2/20/2024
         *-finally fixed, the issue was the code for collision would turn off collision for other directions if colliding. Fixed by adding check for all colliding rectangles
         *-next on list is collision going down isn't smooth
         *2/21/2024
         *-adjusted the look ahead box of left and right, down collision works.
         *2/23/2024
         *-adding menu, the inputs doesn't work. Importing input forgot to add splash state
         *2/24/2024
         *-Switched to splashstate, gameplay isn't loading. Turns out dev mode was on. Turning off Fixed. 
         *-Added inputs for splashstate using imported inputs from earlier.
         *-Settings page isn't loading, a string is null. Added a null check, that returns an empty string. Looks like it might be a part of a greater thing. May have to look at later.
         *2/25/2024
         *-Added a state switch to open menu, but calling back the game state causes the state to restart positions.
         *2/26/2024
         *-Added pause, may need to switch to pausing to open menu after game started
         *2/28/2024
         *-attempted to store position, but it didn't work, may have to write another switch state method that doesn't reload the entire state
         *2/29/2024
         *-messing with events to try and get a gamestate up without switching
         *3/4/2024
         *-using event isn't working since it needs to be not null and the only way to do that is to call a state which messes things up.
         *3/5/2024
         *-somehow made event work, but the reload still happens, trying to add second gamestate to show up only for menu
         *3/7/2024
         *-the hook was using the wrong one causing the previous issue. Can't seem to make menu pop up using second game state in main menu. Will try in current game state.
         *3/10/2024
         *-adding into gamestate works, but the game time stops after resuming and the position is adjusted based on camera
         *3/20/2024
         *-the position is based on the basescreen as that inherits basegameobject
         *-changing the position doesn't change the basegameobject positions that the base screen uses as those are based on 0,0
         *-Fixed by starting another spritebatch.begins that doesn't use the transformation matrix sets the screen back to 0,0
         *-exit button broke on menu in gameplay as the event call is no longer hooked to anything.
         *-fixed by making it a return to title screen button and doing that
         *3/21/2024
         *-Added asset and loading for a comfirm if want to return to title screen. Need to add to splash state to make it pop up
         *3/22/2024
         *-Made it pop up, but no button doesn't work, also back button doesn't work as intended. Previous screen needs to work better
         *-added stack to control the menus, making the back button work. Pop up no is also fixed too. The pop up only appears on upper left corner though
         *3/24/2024
         *-added position to try and make the pop up appear center. Needs to find the render that places it there
         *3/25/2024
         *-Fixed by implementing a position variable in  base screen and  have the splash screen provide the position based on viewport size
         *-may be next to make a settings screen?
         *3/26/2024
         *-started preparing to add text to display on settings screen
         *3/27/2024
         *-the text classes require a font and passing font from state to screen to text seems like its not the right way. needs more thinking.
         *3/28/2024
         *-Added constructors for font and only pass font when text is needed. Text appears, but not in right place.
         *3/29/2024
         *-passing half width and third height to position of text made it appear in right place
         *3/30/2024
         *-text from settings persisted between screens. Turns out the call was only made when changing screen, not removing screen. Fixed.
         *3/31/2024
         *-added multiple enums to handle text for settings. Also added the settings for fullscreen, windows, and borderless. Borderless doesn't work completely. Need resolution to work
         *-added resolution for 1080 and 720, but menuscreen doesn't have a 1080version so it looks weird
         *4/1/2024
         *-Added control remap screen, ran into issue where text would not generate for settings when going back leads to settings. Fixed by adding settings in remove screen method
         *4/2/2024
         *-added text to remap screen for the different actions, but not keys
         *4/3/2024
         *-added text for keys to remap screen, added confirm remap screen. Removed arrow when at confirm screen. Need to figure out way to pass action and key to remap
         *4/6/2024
         *-added code to handle remapping, but running into issue where remapping happens on press, but once you release, it'll restart the cycle.
         *4/7/2024
         *-added a done screen, it disappears on release, but it stops the loop. The string doesn't update though. Fixed by reloading the remap screen
         *-remap only changes confirm action. Fixed issue by removing some old code that wasn't cleaned up. The loop fix isn't going to fly though since remapping for other buttons doesn't have this issue
         *-Fixed loop issue by using a bool to control confirm button presses with screen changes. Next is 1080p screens?
         *4/8/2024
         *-Added screens for menu, title, and settings for 1080. Ran into issue where changing resolution doesn't change resolution until splash screen exits.
         *-turns out, the resolution is done at constructor of the screen. Need to figure out around that.
         *4/9/2024
         *-solved by adding a, initialize method for each screen that returns a new screen based on the screen the initialize method is in
         *4/10/2024
         *-switching to 720 in title, then changing to 1080 in game, then going back to title causes to stay in 720 screens
         *-Fixed by passing resolution to method that returns to title screen
         *-4/12/2024
         *-issue with borderless, it needs to strech to fit screen size
         *4/13/2024
         *-_renderTarget in main game handles this, but passing this might not be right call.
         *-maybe better to set resolution on fullscreen/borderless to screen resolution that fits the screen
         *4/14/2024
         *-added check for resolution that applies on borderless. Is hardcheck, but it'll do for the scope of this project
         *-added SE and BGM, added asset for volume bar
         *4/15/2024
         *-Made sound bar display, and remove soundbar on leaving setting screen, partial add to changing volume
         *4/16/2024
         *-Added changing volume number. However, it stops working once you go out of range.Fixed by setting max and min values for volume.
         *4/17/2024
         *-Added moving effect for both bar and arrow for volume when changing volume. Maybe add volume that changes on game boot as well for future use as well
         *4/18/2024
         *-Added volume for Sound Effects(SE). Volume isn't carrying between states. Also changing volume for one changes colume for both.
         *-Turns out mastervolume is by ref. Needed to use soundeffectinstance to separate the two.
         *-For the volume not carrying, loading a track set it to it's original volume. Fixed by adjusting volume on load.
         *4/19/2024
         *-aligned volume bars and texts. Added BGM and SE text. Added jagged arraylike to arrow cap allowing limited selection in menu
         *4/20/2024
         *-SE volume control was working. Fixed by adding function for SE as soundmanager handles SE differntly. Meaning, I only need one soundmanager. Removed one of the sound manager
         *-volume display doesn't carry over between screens. Fixed by removing initial sound setter. But now unable to adjust sound before game launches. May save for launch settings implementation
         *-returning to title causes a null issue with tracks. Fixed by unloading soundtrack on return to title. Added beeps to moving in splash screen
         *4/21/2024
         *-added holding down to change volume
         *-started implementing mouse and controller input
         *4/22/2024
         *-Mouse is set differently than keyboard. Figuring out implementations allow for remapping the mouse controls.
         *4/23/2024
         *-more mouse stuff
         *4/24/2024
         *-added extension method for mousestate to make it handle more similar to keyboardstate
         *4/25/2024
         *-started adding mouse controls to settings
         *4/26/2024
         *-more mouse implementations
         *4/27/2024
         *-added class to handle mouse positions over buttons and added rectangles for buttons
         *4/28/2024
         *-Ran into issue where boxes weren't detecting mouse position, mouse state passed was not ref. Added in update, will need better way of getting this though
         *-Another issue where only the first box is dtecting, the rest arent. The return in the checker breaks the loop, made if and only return true if passes if.
         *4/29/2024
         *-Removed mousestate passing from splash state. Setting up click to work
         *4/30/2024
         *-Clicking now works on the title screen. Added Settings screen rectangles. Also there is issue with click where clicking anywhere will pick the last selected option even if no longer hovering over an option
         *5/1/2024
         *-Made click not work unless over a button. Removed the one empty box in settings
         *5/2/2024
         *-Added an abstract class to handle buttons since theres alot of repeated code
         *5/3/2024
         *-Changed screens to use abstract class, need to work on adding mouse control for volume bar and add mouse for remap
         *5/4/2024
         *-Added text for mouse remap
         *5/5/2024
         *-remap screen needs the entire first column to be unclickable as the second and third columns are for keyboard and mouse respectively
         *5/6/2024
         *-created new object for rectangle and made it possible to be readonly. When readonly, mouse collision is turned off. Will need to add for keyboard input too
         *-also, will need way  to mark as readonly
         *-rectangle generation still needs work, only works from top left corner, needs way beyond that
         *5/7/2024
         *-Adding class for handling keyboard movement/input
         *5/8/2024
         *-more work on the class to handle keyboard
         *5/9/2024
         *-finished the method for movement. Now works with readonly rectangles by skipping over them. Also for some reason, X and Y are reversed
         *5/10/2024
         *-added mouse methods to help with remapping in extensions
         *5/11/2024
         *-issue with remapping where trying to remap mouse keeps selecting scroll up regardless of button pressed
         *5/12/2024
         *-turns out the scroll up was a text printing issue as the texts didn't match up. Also it isn't printing the correct remap
         *-will need to add swapping remap keys as that's not in yet. Added in for key, may need confirm box
         *5/13/2024
         *-Added remapping for mouse. Ran into issue where the text kept appearing one up after remapping left click. Fixed by adding empty text
         *5/14/2024
         *-Added pop up screen. Ran into issue where text was not displaying correctly if there was a null text in the middle of the list
         *-fixed by moving the null check after the column/row check in positioning
         *5/15/2024
         *-RemapControlDuplicate doesn't work, it continues with the back commands
         *5/16/2024
         *-Fixed text issue where the pop up wouldn't change text. Also fixed passing input control to pop up so now it displays action and click when selecting duplicate click
         *5/17/2024
         *-Fixed issue where the remap swap dupe wasn't working and kept repeating. Used bool to control click
         *-need to implement multiple pop ups though, also text for the pop up screens stays after the popup goes away
         *5/18/2024
         *-turns out the pop ups aren't working for remap again. may have to implement the remap pop ups are popups, then work on fixing
         *5/19/2024
         *-turns out the GetMenuCommand was sending 1 instead of 0, and 1 has no value. Changed so it works.
         *5/20/2024
         *-fixed issue of repeating remap swap popup by removing it from stack after confirming swap, but before done screen added
         *-the bool to not remove text on screen change maskes it so the text doesn't get removed at all
         *-also, the swap remap isn't working again, turns out the code to remap is built into the click itself. Will need to remove and make a callable method. Perhaps store in inputdetector as temp
         *5/21/2024
         *-swap remap works now, added as temp holders in input detector and added the extra remap instead of just one remap
         *-also the text isn't refreshing on remap.
         *5/22/2024
         *-Fixed text refresh by moving the refreshing of screen from inputmapper to splashstate
         *-Fixed some more issues like extra rectangles on yesnopopup, back not working on remap control screen, and clicking on remap done screen
         *-Selecting no on swap confirm makes go back to enter button screen not work. Fixed by adding another back to rid enter  button screen
         *5/23/2024
         *-Added swap remap for key, but pause remap doesn't work. fixed, needed to add a call to remapcontroldone after remapping
         *-Added method in keyboard handler to account for readonly rects on screen load. Still a work in progress.
         *5/24/2024
         *-more adjusting the method, currently it moves opposite x,y directions. ned to fix.
         *5/25/2024
         *-fixed, the recursion with returns don't work at all. Turned into for loops and swapped the x,y on return. Also removed else from x/y checker. It's working now.
         *-next issue is keyboard input to circle around ignores readonly
         *5/26/2024
         *-fixed, added code to check for readonly when when keeping arrow in bounds
         *-ran into issue where the the keeping bounds was calling even when screen has no buttons, cause crash. Fixed by adding checker for button screens.
         *5/27/2024
         *-Volume bar didn't bar on going back. Fixed by adding the settings loader on remove screen.
         *-adding code to made volume bar work with click, ran into issue where overlapping up on keyboard to volume bar stack overflows
         *5/28/2024
         *-fixed by adding check if xposition is greater than the x buttons that exist, return 0 which makes it do nothing
         *-found another issue where moving up from the right most resolution option causes index out of bounds issue that the previous statement was supposed to solve
         *5/29/2024
         *-fixed by adding check for out of bounds and setting yposition to max if negative then moving up.
         *-added clicking to adjust the volume for BGM, SE broke and clicking isn't working. Turns out the title commands wasn't adjusted when new options were added. Fixed now.
         *-Now to add holding clicks, ran into issue where clicking once makes it think it's holding the entire time
         *5/30/2024
         *-fixed, the removal of holding clicks didn't actually remove holding clicks. It does now.
         *5/31/2024
         *-working on adding a settings save so it can carry over between sessions
         *6/1/2024
         *-need to pass values to text file somehow
         *6/2/2024
         *-more work on serialization
         *6/3/2024
         *-more work on passing values
         *6/4/2024
         *-created class to store values that gets passed and a manager to handle savng loading and creating
         *6/5/2024
         *-creating a settings file using defaults work now
         *6/6/2024
         *-passed most of the info to the Settings data, need to figure how to pass remap data. May need to pass entire thing anyways
         *6/7/2024
         *-Passed entire controls and added method to save setting data and set saving to changing game state and exiting game
         *6/8/2024
         *-created manager class to handle settings change to help with loading settings from file on boot
         *6/9/2024
         *-Settings are now loaded based on settings data text file
         *-will need to adjust boxes for clicking to cover words now
         *6/10/2024
         *-fixed boxes to cover words, need to fix position for remap screen as words overlap arrow
         *6/11/2024
         *-fixed issue where holdiing mouse click made it click through screens by disabling holding clicks for actions that it wasn't needed
         *-fixed remap arrow overlapping issue
         *6/12/2024
         *-Adding input for gamepad, so far added action button and input detector version for gamepad buttons
         *6/13/2024
         *-added extension to gamepadstate for more methods
         *6/14/2024
         *-finished adding gamepad to code, no gamepads available at the moment to test. Next to add text to remap screen
         *6/16/2024
         *-Added text for the gamepad controls to the remapscreen. no controllers to test atm.
         *6/20/2024
         *-next part is adding gameplay
         *6/22/2024
         *-starting with adding items to game
         *6/23/2024
         *-more item
         *6/24/2024
         *-more item
         *6/26/2024
         *-more item
         *6/28/2024
         *-more item
         *7/1/2024
         *-more item, added player and inventory
         *7/2/2024
         *-reworked some item added more to player
         *7/3/2024
         *-added spoon to draw, but is not displaying. Fixed by activating object. Added collision to spoon, adding interact text when colliding
         *7/4/2024
         *-added pop up box for interact
         *7/5/2024
         *-drew popup + text and made appear when collding with interactable item, need to tie to character position or camera
         *7/10/2024
         *-added way to store collided items
         *7/11/2024
         *-need to find to add item to inventory
         *7/12/2024
         *-added to inventory need to test
         *7/15/2024
         *-working on manager for popups, working on loading texture
         *7/16/2024
         *-made text appear with pop up so no need to add both to game objects
         *7/17/2024
         *-text in settings was hidden, turns out they need to be activated. Going into remap throws null, added checker for null
         *-need to confirm if add to inventory works
         *7/19/2024
         *-adding timer to make add to inventory pop up disappear
         *7/21/2024
         *-timer goes too slow, need to adjust
         *7/24/2024
         *-trying to get timer to work to make popup disappear
         *7/29/2024
         *-got timer to work by passing gametime to activate method
         *-testing UI positions
         *8/1/2024
         *-got popup box to move with screen, now need to make text move and also all the update stuff to updates
         *8/4/2024
         *-linked text position to box position and changed positions to appear center left. Moved code for updating from the popup box to the manager
         *-next is to account for multiple popup boxes
         *8/5/2024
         *-created list, but object in list isn't being added to game object list, may have to object pool
         *8/9/2024
         *-found that new popups in list isn't being added to game objects. Added and texture not loading. Fixed by loading needed texture and passing to manager
         *-made it so max of 10 add inventory popups after that it grabs oldest popup and overwrites that. Next is to make positions work 
         *8/10/2024
         *-added by multipling space with list count then subtracting space
         *-remove item from map on pick up
         *-next is drawer?
         *8/25/2024
         *-adding drawer
         *8/26/2024
         *-more drawer, also instead of a texture2d, drawer may need to be a sprite sheet since it'll need animation for opening
         *8/28/2024
         *-implementing more to make spritesheet work
         *8/29/2024
         *-will need facing directions in case object is direction specific, ie cannot be opened from this side
         *-can render drawer and implemented as collidable object
         *8/30/2024
         *-moving collision code to collidable object
         *9/2/2024
         *-attempting interact based on direction
         *9/3/2024
         *-more directional interaction
         *9/10/2024
         *-messing with actions
         *9/16/2024
         *-will need new collision to detect interactable objects
         *9/19/2024
         *-Added to game object list to make interactable, but disable doesn't work to remove interactable and collidable
         *9/20/2024
         *-Added checks in collision to run if interacable or collidable, updated activate as well
         *9/23/2024
         *-Added more manager to handle multiple objects
         *9/24/2024
         *-combined the item and overworld manager since they serve similar functions and the list needs to be one list to handle interactions
         *9/25/2024
         *-added interaction direction for objects
         *9/26/2024
         *-added direction for interaction, also made direction detection tighter, still needs more work though
         *-now to get the animation to play
         *10/1/2024
         *-it was playing animation, the draw for bounding box was over the animation, also animation repeats. Moving drawer doesn't cause boundingbox to follow. Fixed, turns out position already calcuated for bounding boxes
         *10/3/2024
         *-Fixed collision not working, turns out the position handling only affects position, not bounding box, will need to override 
         *10/9/2024
         *-fixing position handling
         *10/14/2024
         *-fixed the positioning issue, turns out it's the rect that's used to check collision and that wasnt updating
         *-need to fix animation looping of drawer
         *10/20/2024
         *-Fixed looping issue, it's supposed to not loop by default, but it was. Added method to unloop and called that
         *-restarts loop on interact, added check on animation change. now to add spoon in drawer
         *10/25/2024
         *-added check for spoon when interacting drawer, now to make it only check after drawer is open
         *10/29/2024
         *-May need to add states to interactable object?
         *10/30/2024
         *-Added states
         *11/3/2024
         *-moved drawer opening to state check 0, now to add dialog box
         *11/12/2024
         *-implementing dialog, will need buttons for log/auto/etc
         *11/14/2024
         *-added asset for dialog box
         *11/20/2024
         *-added more dialog box stuff, need to test
         *11/28/2024
         *-dialog box was created with wrong base, will need to fix
         *11/29/2024
         *-mimiced popupbox, maybe need to abstract?
         *12/1/2024
         *-Dialog box displays, need to fix position and text
         *12/2/2024
         *-Fixed box to camera, but need to find to fix to correct position
         *12/4/2024
         *-figured out how to position the dialogbox at bottom
         *12/5/2024
         *-wrote a wordwrapper class to war text in dialog box, but it's missing the last word
         *12/6/2024
         *-finshed wordwrapper, now to make the text appear over time
         *12/8/2024
         *-maybe for loop or somehow incorporate into update
         *12/10/2024
         *-calling to update made it work, speed works too now
         *12/13/2024
         *-may need to add a dialog state to let dialog continue
         *12/22/2024
         *-added static to store dialog, for now
         *12/23/2024
         *-added changing speed on action in mid dialog, closing dialog box at end of text display. next is continuing dialog
         *1/2/2024
         *-adding punctuation wrap to allow sentence separation for text display
         *1/3/2024
         *-attempting test at displaying dialog in a row, but previous text isn't clearing
         *1/4/2024
         *-continuous dialog boxes now work, now for text line limits
         *1/5/2024
         *-set lines of text to 3
         *1/6/2024
         *-added dialog box speed control when adding text, next is user speaking
         *1/8/2024
         *-working on speaker, may need dictionary
         *1/9/2024
         *-need to add separate gameplaytext for speaker to make it independant from text and to get it to display
         *1/13/2024
         *-need to size text and change position after initialization
         *1/14/2024
         *-size can't change after initialization, will need multiple fonts to handle size changes
         *-position of speaker subtitle done, spritefont has a measure string method which returns pixel size of the text
         *1/15/2024
         *-added placeholder for continuing dialog
         *1/16/2024
         *-started on minigame
         *1/18/2024
         *-working on drawers, will make left and right hand separate as well
         *1/19/2024
         *-finished 3 drawers, want 2 more. Added them to display, need to center on player
         *1/20/2025
         *-finished adding drawers, centered them, made go frame by frame, next is player and randomise control
         *1/21/2025
         *-Added random control, kind of. May re-use old sprite or replicate in current system
         *
         */
    }
}
