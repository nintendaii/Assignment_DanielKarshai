# Assignment_DanielKarshai
This project is developed in Unity 2021.1.5f1

# Gameplay
![](https://i.imgur.com/jwTQo4B.gif)
# Architecture
There are 4 main modules:
- `Core` - module with core architecture functionality;
- `Project` - additional to Core module to extend architectures functionality
- `Game` - module with the actual game
- `MainMenu` - module with Main Menu screen

## Note
To be able to use `[Inject]` all our classes should implement `IBindComponent` interface.
For example: 
```c#
public class ScoreController : ComponentControllerBase<ScoreModel, ScoreView>, IBindComponent
...
```
And now we can inject this class to any MonoBehaviour in the scene like this:
```c#
[Inject] private readonly ScoreController scoreController;
```
Classes that implement `IBindComponent` from the same module must be in the scene hierarchy.

