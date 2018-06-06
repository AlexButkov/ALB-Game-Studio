# ALB Studio
This is a free set of tools in C#, which helps you develop games and prototypes in a Windows Console Application.

# Features
- Preset game object classes (ObjectSingle and ObjectGroup).
- Automatic rendering of game objects.
- Available compound objects (ChildList field and ObjectGroup class).
- There are several methods to translate objects in different directions (Controller class methods: MoveByWASD, MoveAside, MoveTowards).
- Available methods to define moments when a collision occurs (ObjectSingle class methods: TriggerEnter, TriggerStay, TriggerExit).

# Game sample
![](https://img.itch.zone/aW1hZ2UvMjY1NTkzLzEyNzk2MzEucG5n/original/eEm5Sh.png)
Explore the SampleGame class, read methods summaries and modify a game logic to see all features in action.

# How to create a new game?
1) Remove the SampleGame file from the project folder.
2) Write a new game logic into Start and Update methods of the NewGame class (or any other, which implements ALBGame class).
3) Build a project in Visual Studio and run the game.
