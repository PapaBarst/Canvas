# Canvas
Canvas is a simple, open-source tool for drawing shapes and images in C#. The goal of this program is to provide an intuitive, easy to use drawing interface for people new to coding in C#, while still allowing higher level manipulation and functionality. 

# Startup
To create an instance of Canvas, begin by extending the CBase class in a class of your own. Next, implement the abstract methods `Draw()` , `DrawOnce()`, and `Setup()` (blank is fine). Finally, add `CBase.Main("namespace", "className")` to your `static void Main` or the entry point of the application.

Once setup is complete, you're finally ready to start creating your own program in Canvas! Below are some explanations of the default methods.

### Draw
The `Draw()` method executes each tick(whenever the graphics refreshes, controlled by the refresh rate). By default, drawing methods in `Draw()` will add objects to the first layer above the background with the layer clearing its drawstack every tick. This behavior can be changed by switching the `retain` property of the targeted layer.

### DrawOnce
The `DrawOnce()` method executes once at the beginning of runtime after `Setup()`. By default, drawing methods in `DrawOnce()` will add objects to the background layer with objects being retained each tick. This behavior can be changed by switching the `retain` property of the targeted layer.

### Setup
The `Setup()` method executes once at the very beginning of runtime before window creation. In this method you can set window properties such as `Width` and `Height` as well as change other properties like `rescaling` and `refreshRate`. 

# Learning
Canvas was designed with learning in mind, allowing students and new learners to visualize their code while slowly introducing them to new ideas. A general learning structure using Canvas could be similar to the following:
1. Learning basic C# syntax (for loops, if statements, switches) using methods like `Rectangle` and `Ellipse` to visualize the results
2. Learning about objects by using a `RectangleC` object and manipulating its properties to move it around the window
3. Learning about Lists and Arrays by manipulating the drawstack
4. Learning about interfaces by looking at the `Drawable` interface
5. Creating custom shapes by creating custom classes and implementing the `Drawable` interface
