# TODO: #
* Missiles
 * Think about using pattern Decorator for missiles and properties
  * Missile Property should work with TargetTexture, and after all it modification draw on screen
    * Add Fire missile property
      * Draw it with shader
* Nodes
 * Think about using pattern "Strategy" instead of using inheritance.
 * Implement Storage node.
* Architecture
 * Inherit classes from DrawableGameComponent instead of updating and drawing it from main Game class.
   * NodeMap
   * TileMap
* Animation
 * Find sprite animation library(Need to look at source code of some others XNA games, such as 'Terraria').

* (done)Implement node map.
  * (done)Use node map for finding next nodes.
* (done)Implement Draw method for nodes.
* (done)Create 'object pool' for NodeData objects.
