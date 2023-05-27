# Overview

This project includes a useful "TileMap" class to use for games made with the MonoGame framework. 

# Development Environment

I did this project using Visual Studio and the MonoGame framework. It is coded in C#.

# How to use:

To use this asset, follow the following steps: (for more detail, see [link](linkgoeshere))

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
Chunk, Content folder name, txt file folder name, txt file name, tile size, x coordinate of the chunk, y coordinate of the chunk.
```
Descriptions of each thing:
- Content folder name: this is the folder within the special MonoGame "Content" folder where you have all the files for this specific chunk. For example, you may have put the red brick files in a folder called, "redBuilding" inside of the content folder.
- txt file folder name: this is the folder where your chunk .txt file is located. In this sample project, the chunk files are found in the ChunkFiles folder, but maybe you create a folder called "Buildings" or "Lawns" or whatever types of chunks you are creating.
- txt file name: this is the name of your chunk .txt file. For example, you might have named it "redBrickBuilding.txt"
- tile size: This is the number of pixels of each tile. To have a very large chunk, you can put like 500 or something. The width and height of the tile will be this value.
- x coordinate of the chunk, y coordinate of the chunk: This is an x,y coordinate where the top left hand corner of the chunk will go, relative to the top left corner of your window. Note: you can put negative values here to have your chunk render to the left or above the window.

## 5. Put your tilemap into the game

Now that you have done the hard stuff, it's time to put your tilemap into the game. If you are familiar with how the MonoGame framework works, this should be pretty intuitive.

1. Create and initialize the Map variable in your game class:
```cs
public class Game1 : Game
    {
 
        private Map map;

        public Game1()
        {
            // create map object
            map = new();
            map.CreateChunks("MapFiles/map1.txt");
```
2. Impliment the "CreateChunks" method right below where you initialized the map object
```cs
public Game1()
        {
            // create map object
            map = new();
            map.CreateChunks("MapFiles/map1.txt");
            
```
3. In each of the MonoGame built in functions, (Initialize, Load, Update, and Draw,) call the same functions in the map object:
```cs
protected override void Initialize()
{
  base.Initialize();
  
  // note: it is important that map.Initialize goes BELOW base.Initialize().
  map.Initialize();
}

protected override void LoadContent()
{
  map.Load(Content);
}

protected override void Update(GameTime gameTime)
{
  map.Update(gameTime);
  
  base.Update(gameTime);
}

protected override void Draw(GameTime gameTime)
{
  // note: here we have to pass our SpriteBatch type object to the draw method.
  map.Draw(_spriteBatch);
  
  base.Draw(gameTime);
}

```

Run the code, and enjoy!

## Using a chunk instead of a tilemap
P.S. The chunk class acts almost exactly like the tilemap class. if you prefer to just make one giant chunk for your tilemap, you can do so. Everything about implimenting it into the main Game1 file is the same except for initializing the variable. To initialize a Chunk object, you need to pass in the following parameters:
```cs
public Chunk(
            string textureFolder,
            string fileFolder,
            string fileName,
            int tileSize=100,
            int chunkPosX=0,
            int chunkPosY=0
            )
```
It's basically the exact same thing that you put into the TileMap .txt file, but you have to code it in instead. 

# Useful webistes
* [Stackoverflow: How to resize a texture](https://stackoverflow.com/questions/4349590/resize-and-load-a-texture2d-in-xna)
* [Using the MGCB editor](https://docs.monogame.net/articles/content/using_mgcb_editor.html)

# to do:
* instead of the CreateChunks method in the map class, simply put the file in the constructor
* make the chunk txt files more robust like the TileMap files. For example, allow comments using #, and allow blank lines. 
