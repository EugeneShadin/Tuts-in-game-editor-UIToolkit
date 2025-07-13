### [Part 1: Toolbar Panel](https://medium.com/@e.a.shadin/in-game-editor-with-ui-toolkit-part-1-toolbar-6f55a4686509)
Create a basic toolbar with buttons and a minimal style sheet.


### Try implementing the following improvements:

* Create a new class called UIToolbarGroup, so that buttons can be created directly inside a group. UIToolbar will now only operate with groups.
* Add a new style selector named toolbar-secondary to distinguish secondary button groups with a different background colour.
* Move the background colour from the toolbar-button selector into two new selectors: toolbar-button-primary and toolbar-button-secondary. This will make future styling easier.
* Add support for a new style selector toolbar-button-wide — set a fixed width (e.g., 140px) to highlight more important actions.
* Update UIToolbarGroup to support the new style selectors.
* Try creating multiple toolbars — one at the top, and another at the bottom of the screen.
* ⭐ Bonus challenge: Create a few vertical toolbars and place them between the horizontal ones. The orientation should be configurable via UIToolbar.
