# Unity RTS Character Selection

This is a basic example of a way of handling character selection in Unity.

![Sample](https://raw.githubusercontent.com/abban/rts-character-selection/master/sample.gif)

## Requirements
In order to run this example you'll need to install [Zenject](https://github.com/modesttree/Zenject). If you don't want to use that then it should be possible to pull the code into MonoBehaviours.

This project uses a basic NavMesh for moving the characters, but you can use whatever you like.

## How It Works

### Input
The input controller will run once a frame on Tick and set variables an input state object depending on what buttons the player is clicking.

### Controls Controller
The ControlsController also runs one a frame but on LateTick, it will use the input state object and has various handlers for the different things it needs to do:

* ControllingHandler: Handles events when the player is not selecting.
* SelectingHandler: Handles events when the player is selecting.
* SelectionDragGuiHandler: Handles drawing the selection box.
* CursorHandler: Handles setting the cursor depending on what's being selected.

### Player
The player files have a basic selection handler and movement handler in order to perform selection and movement events.

* MovementHandler: Tells the NavMesh Agent to move to a point or stop.
* SelectionHandler: Sets selecting/selected/deselected on a player.
* PlayerRegistry: This is a utility to give other parts of the system access to existing player characters.

### Camera
The camera files just contain a handler for raycasting.

* RaycastHandler: Fires a ray at a screen position and returns a RayCastHit.
