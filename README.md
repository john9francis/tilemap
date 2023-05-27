# Overview

This project includes a useful "TileMap" class to use for games made with the MonoGame framework. 

# Development Environment

I did this project using Visual Studio and the MonoGame framework. It is coded in C#.

# How to use:

To use this asset, follow the following steps:
1. Get some images you want to use as tiles. (some sample ones are found in the project.) These will be put into the MonoGame content pipeline, so make sure the images are an acceptable format. 
2. Create a folder in the MonoGame content MGCB editor to put your tile images, and then import the images into that folder. (see **useful websites** for how to add to the content editor)
3. Create a "chunk" .txt file. A chunk is a group of tiles that you can use to build your tilemap. Think things like buildings, road segments, simple building blocks of your complete scene. You might want to have some "reusable" chunks like a building that will show up several times in your map.
  A chunk .txt file is going to be a bunch of the names of tiles, seperated by commas. For example, if you have tiles called "red_brick1," and "red_brick2," your .txt file would look like,
  ```
  red_brick1,red_brick1,red_brick2,0,0
  red_brick2,red_brick1,red_brick1,red_brick2,red_brick2
  red_brick1,red_brick2,red_brick1,red_brick2,red_brick2
  
  ```
  This chunk could be a small red brick building with the top right corner broken off. The zeroes will simply not render tiles in those spots. 

# Useful webistes
* [Stackoverflow: How to resize a texture](https://stackoverflow.com/questions/4349590/resize-and-load-a-texture2d-in-xna)
* [Using the MGCB editor](https://docs.monogame.net/articles/content/using_mgcb_editor.html)
