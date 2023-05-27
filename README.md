# Overview

This project includes a useful "TileMap" class to use for games made with the MonoGame framework. 

# Development Environment

I did this project using Visual Studio and the MonoGame framework. It is coded in C#.

# How to use:

To use this asset, follow the following steps:

## 1. Get some images you want to use as tiles
(some sample ones are found in the project.) These will be put into the MonoGame content pipeline, so make sure the images are an acceptable format.

## 2. Import your files into the MonoGame content pipeline
Create a folder in the MonoGame content MGCB editor to put your tile images, and then import the images into that folder. (see **useful websites** for how to add to the content editor)

## 3. Create some chunk .txt files
A chunk is a group of tiles that you can use to build your tilemap. Think things like buildings, road segments, simple building blocks of your complete scene. You might want to have some "reusable" chunks like a building that will show up several times in your map.

A chunk .txt file is going to be a bunch of the names of tiles, seperated by commas. For example, if you have tiles called "red_brick1," and "red_brick2," your .txt file would look like,
```
red_brick1,red_brick1,red_brick2,0,0,
red_brick2,red_brick1,red_brick1,red_brick2,red_brick2,
red_brick1,red_brick2,red_brick1,red_brick2,red_brick2,
```
This chunk could be a small red brick building with the top right corner broken off. The zeroes will simply not render tiles in those spots. 
  
When the program renders your chunk, it's going to search the content folder in your project for these filenames. If you have a filename in your .txt file that is not in the content folder, it will simply not render a tile in that spot.

Note: chunks are rectangular. Your first line will dictate how many tiles long the rest of the rows will be. For example, if you have a chunkfile that looks like this:
```
01,01,01,01,01,01,01,
02,02,02,02,
03,03,03,03,
```
The chunk will render as if you had input:
```
01,01,01,01,01,01,01,
02,02,02,02,03,03,03,
03,00,00,00,00,00,00,
```
If you want a different shaped chunk, make sure to utilize zeroes to cushin in the filenames so you can visualize what you are doing. 
 
## 4. Create a map .txt file
Once you have created a couple of different chunks, create a map .txt file. This one is a little bit different than a chunk .txt file. The map file gives you more freedom. Chunk files force all the tiles to be the same exact size. For your tilemap, you can have chunks of different sizes and shapes, close together or far apart, it's your choice. Here is how you do the map .txt file:

Start the line with "Chunk". This let's the program know that you want to create a chunk. Then, the file follows this format:
```
Chunk,
```

# Useful webistes
* [Stackoverflow: How to resize a texture](https://stackoverflow.com/questions/4349590/resize-and-load-a-texture2d-in-xna)
* [Using the MGCB editor](https://docs.monogame.net/articles/content/using_mgcb_editor.html)
