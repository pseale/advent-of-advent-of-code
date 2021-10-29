# Advent of Code, 2015

Welcome! These are my solutions in C#, constructed in JetBrains Rider.

### Goals

- Become comfortable with JetBrains Rider, and harvest my keybinds once I am comfortable
- Familiarize myself with C# 9.0 (.NET Core 5.0) - in practical terms, this means records
- Familiarize myself with Advent of Code problems and get enough experience to be able to metaphorically 'hum along to the tune' - in practical terms, this means
  - Project structure
    - each day is its own console app
    - one big test project with end-to-end tests that are (if feasible) direct copies of the example problems
  - Settle on 'parse non-empty lines from input.txt into string[]'
  - Solve PartA and PartB without trying to modify the existing PartA code - this approach allows for code reuse, but doesn't require it

### Apologies

This is not an exhaustive list. I have many things of which to be sorrowful.

- Usually I'll make an inline apology comment, especially if I'm making a particularly intense (intensely unnecessary, and possibly harmful) abstraction. The best example so far is `Day06`'s `Do<T>(Func<T,T>)`. Just know that I won't subject a team to this kind of thing.
- I am not trying for efficiency. E.g. `Day05`'s `HasPairWithoutOverlapping()` - I'm aware that my implementation is VERY inefficient.
- I'm enjoying writing unnecessary LINQ pipelines. I am neither extremely for, nor extremely against using LINQ over foreach/if. I do care, rarely, in some cases. I can write the code however is preferred. I'm not crazy! I can be just as crazy as needed--no more, no less. If you're crazy about LINQ--I can pretend! If you hate LINQ--I can also pretend!

### Notes

- I've been pleasantly surprised by how many times I actually (truly!) solved the problem on the first try, after compiling and passing the basic end-to-end tests.
- With that said, I have made many, many, many mistakes, and most of those mistakes were due to me misunderstanding the problem.
- I am exercising my 'problem solving with code' muscle, and it hurts.

### Complaints about Rider

These are mostly small nitpicks:

- 2021-10-27: A Prettier-style Cleanup on Save is not available. I've become addicted to autoformatting from VS Code. I don't remember to run Cleanup manually, and I don't want to add a git hook. VS Code does this correctly--copy them!
- 2021-10-27: Missing "Close Other Editors" hotkey, like I can run in VS Code. To be fair, Visual Studio can't do this either, and I'm guessing because (in both JetBrains and Visual Studio's case) the user is allowed to do this on any file.
- Window management is awkward. I've bound a keystroke to `Hide All Tool Windows`, but I think it should be easier? Honestly, I don't understand what the 'docked/undocked' toggle does. It seems to do nothing. So chalk this one up to general confusion.
- Rider fighting Rider in `.CSPROJ` files - create a New Project using Rider's wizard, which uses 4 space indentation, then run Cleanup, which changes the 4 to 2.
- I know I'm in the Complaints section, but I must give Rider kudos for being fast. Kudos JetBrains, kudos.
