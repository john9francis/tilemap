# Overview

This project includes a useful "TileMap" class to use for games made with the MonoGame framework. 

# Development Environment

I did this project using Visual Studio and the MonoGame framework. It is coded in C#.

# How to use:

To use this asset, follow the following steps: (for more detail, see [these detailed instructions](instructions.md))
1. Get some images you want to use as tiles
2. Import those images into the MonoGame content pipeline
3. Create some chunk .txt files
4. Create a map .txt file
5. Impliment the Map object into the main game file using the map .txt file
6. Enjoy your TileMap!


# Useful webistes
* [Stackoverflow: How to resize a texture](https://stackoverflow.com/questions/4349590/resize-and-load-a-texture2d-in-xna)
* [Using the MGCB editor](https://docs.monogame.net/articles/content/using_mgcb_editor.html)

# to do:
* instead of the CreateChunks method in the map class, simply put the file in the constructor
* make the chunk txt files more robust like the TileMap files. For example, allow comments using #, and allow blank lines. 
