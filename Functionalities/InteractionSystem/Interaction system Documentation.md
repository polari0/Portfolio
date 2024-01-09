This markdown file documents the different aspects of Last minute shopping Interacion system
This version of the interaction system is not in the final version of the game but if you would like to use it please also check out my version of the First person character controller as this was build to work with it

# Classes

All of the Following classes are Under InteractionSystem NameSpace

1. InteractionHandler
2. PickupableObject
    1. Any DerivedClass/object that can be picked up
3.  IInteractable
    1. IInteractablePrimary
    2. IInteractable2

# Enums
1. Object Type 
2. Object Property


## Interaction Handler 
Holds all the logic on what to do when you interact with elements that can be interarcted with as well as logic to check if you can interact with object you are pointing at. 

### Public Methods 
None

### Public Variables 
None
#### Editor Visible Variables 
1. playerCamera 
This is the camera attached to player transform. Used for vision as well as raycast location

2. characterControllerActionAsset
reference to Input actions 

3. interactRange
Tells from how far player can Interact with Objects 

4. interactableIndicator
UI Indicator for when you can interact with items

5. distance, smooth
2 float Variables that determine the following
    Distance = How far player holds picked up item from them 
    Smooth = How many times held item position is updated

6. powerMutilpierForward/Up
2 int Values determining the following 
    PMF = How much force to forward direction should be apllied 
    PMU = How much force to Up direction sohuld be apllied


## PickupableObject
Base class for objects that can be picked up

### Public Methods 
1. Virtual float CalculateEffectOnPlayerSpeed
This method is used to calculate slow effect on item pickup
returns float value based on item weight. Given float value is reduced from player speed while player holds the item. 
If needed this method can be overriden by inhering class but the base class also provides caculation for it (Math to be done to figure it out)

### Public Variables 
1. ObjectType 
Enum that sets object to be either Box or a Tool 
required for the interaction handler to figure out what to do with picked up object
Read more on Section about Emums.
(Not fully implemented yet)

### Editor Visible Variables 
1. Private protected proterty 
Emun that sets object property. This changes how picked up items should act them they are trown and dropped. Read more on Section about Emums. 

#### PickupableItem derived classes 
All the following classes will also have either IInteractalbePrimary or IInteractable2 interface attached to them. These are explained better later on. 

1. Box 
This class contains all the logic controlling boxes and how they should behave when picked up by the player. 


## IInteractable
Base Interface for the Interface part of the Interation system 

### IInteractablePrimary 
For calling what to do when player presses Primary interaction button. 

#### Public Methods 
1. Interact 
Method called when object is interacted with each object using primary Interact button that implements this method should have their own version of it

### IInteractable2 
For calling what to do when secondary interact button is pressed

#### Public methods 
1. SecondaryInteract
Method called when object is interacted with each object using secondary Interact button that implements this method should have their own version of it

## Enums 

### ObjectType 
Depending on the object type Interaction Handler will behave bit differently on the object this. Main purpose for this distiction is to allow trowing boxes and use the interaction for other objects instead. 
1. Box 
2. Tool

### ObjectProperties 

1. fragile 
This kind of boxes can and will break if you trow them to ground too fast 
2. towpBox 
Needs 2 Players to carry the box. Multiplayer Functionality is yet to be implemented 
3. Bouncy 
Box starts to jump around when trown 
4. Soft 
Box is squishy, Doesn't jump and move much when it hit's ground 
5. Slippery 
Box starts sliding around quite easily. 