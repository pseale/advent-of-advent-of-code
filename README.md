# Advent of Code, 2015 and 2021

Welcome! These are:

- my 2015 solutions in C#, constructed with JetBrains Rider
- my 2021 solutions in C# 10, constructed with JetBrains Rider EAP.

### 2015 Goals

- Become comfortable with JetBrains Rider, and harvest keybinds and settings once I'm comfortable
- Familiarize myself with C# 9.0 (.NET Core 5.0) - in practical terms, this means using records. I'm dubious on the value of the pattern matching syntactic sugar in C#, but in theory all that's on the table too.
- Familiarize myself with Advent of Code problems and get enough experience to be able to metaphorically 'hum along to the tune' - in practical terms, this means
  - Project structure
    - Favor 25 console apps, one per day.
    - Use one big test project with end-to-end tests that are (if feasible) direct copies of the example problems. Discussion about testing is discussed in "Thoughts" below.
  - Solve PartA and PartB--both at once--without necessarily trying to modify the existing PartA code. This approach allows for code reuse, but doesn't force it.

### Apologies

This is not an exhaustive list. I have many things of which to be sorrowful; some are listed below.

- Usually I'll make an inline apology comment, especially if I'm making a particularly intense (intensely unnecessary, and possibly harmful) abstraction. The best example so far is 2015's `Day06`'s `Do<T>(Func<T,T>)`. Just know that I won't subject a team to this kind of madness (unless instructed).
- I am not trying for efficiency. E.g. `Day05`'s `HasPairWithoutOverlapping()` - I'm aware that my implementation is VERY inefficient. If it works and is not slow enough to be noticeable by humans, I'll leave it alone.
- I'm enjoying writing unnecessary LINQ pipelines. I am neither extremely in favor of, nor extremely opposed to using LINQ over foreach/if. I can write the code either way--whichever you prefer. I'm not crazy! I can be precisely as crazy as desired. If you're crazy about LINQ--I can pretend to be as crazy as you! If you hate LINQ--I can also pretend to be that specific kind of crazy!

### Interesting, Sometimes Surprising, Thoughts

- I've been pleasantly surprised by how many times I actually (truly!) solved the problem on the first try, after compiling and passing the basic end-to-end tests.
- With that said, I have made many, many, many mistakes, and most of those mistakes were due to me misunderstanding the problem. I partly blame myself, but I also blame others! In all cases where I misunderstand the problem, my automated tests based off of examples from the problem pass!
- I am exercising my 'problem solving with code' muscle, and it hurts.
- Up through day 07, I solved the problems without breaking down my end-to-end tests into smaller unit tests. In Kent Beck TDD terminology, I am going straight to Obvious Implementation, and I don't need any kind of Triangulation to break down the problems. As I encounter more difficult problems, as I did in Day 08, I assume I will need to rely more on unit tests (and Triangulation) to survive.
- I think I'm settling on the idea of substituting a REPL or a debugger (or a REPL in the Immediate Window, while the debugger is running) instead of writing microtests. I probably need to look into what's available nowadays REPL-wise.
- I have, at least temporarily, given up on writing my own algorithm implementations. E.g. for day 13, I needed a simple permutation implementation. First I wrote my own, then when it was buggy and also ugly to the eye, I went googling and copy/pasted a `Permutate()` method from StackOverflow. I don't learn as much doing this, but that's okay.
- Angry at math - 2015's Day 19 and Day 20

### Complaints about Rider

These are mostly small nitpicks:

- Window management is awkward. I've bound a keystroke to `Hide All Tool Windows`, but I think it should be easier?
- Ok: so if I use the `Run` dialog, and also when I run tests, I can't use `Esc` to **consistently** close the window. Looks like someone has reported the same basic problem: https://www.jetbrains.com/help/rider/Run_Tool_Window.html#context-menu-commands
- Rider fighting Rider in `.CSPROJ` files - create a New Project using Rider's wizard, which uses 4 space indentation, then run Cleanup, which changes the 4 to 2. Or is it the spacing of XML nodes? Whatever it is, it is tool-on-tool violence, and I'm just an innocent bystander.
- `CTRL-T, D` hotkey only works some of the time? Hotkeys should work all of the time, especially if sibling hotkeys (such at `CTRL+T, T` `CTRL-T, Y`, `CTRL-T, L`) work all the time?
- I assume we're stuck with Rider's known (expected) behavior, but it sure seems that C# formatting is waging a war against itself. Most notable: Rider adds braces to an if statement, and then on cleanup, removes them. Yes, I guess I can customize Rider's braces behavior. It also fights itself in CSPROJ file spacing.
- I know I'm in the Complaints section, but I must give Rider kudos for being fast. Kudos JetBrains, kudos.

# Advent of Code 2020

Advent of Code 2020 is imminent! Prepare ye the way!

## Goals

- [✔]To stream, as early as January 2020.
- [❌❌❌🟡🟡]To familiarize myself with .NET 5.
- [✔✔✔🟡🟡]During this special time of Advent, in which we celebrate the coming of Advent of Code 2020, in the spirit of reverence and fastidious self-contemplation, solve as many as five (5) Advent of Code 2018 challenges.
  _ [✔]Clarifying rule: I may choose any of the 2018 problems, and I may choose to solve one or both stars. It's not my fault if I declare vendetta against some of the more frustrating "fun puzzles".
  _ [✔✔❌🟡🟡]Do something different! \* [✔✔✔✔✔]Code golf is garbage (❌⛳❌). Let's not do code golf.
  > > > > > > > 9696054... solved day 5 using wit and guile (cheating)

## Reflecting on estimation

Nobody is good at estimation. Even me, superman. I've logged my estimates that prove Hofstadter's Law: It always takes longer than you expect, even when you take into account Hofstadter's Law.

<<<<<<< HEAD

- Day 01 (cheating)
  - Estimate: 5 minutes
  - Actual: 30 minutes
  - Unexpected landmine: cheating is harder than it seems. You have to work surprisingly hard to cheat, because code samples don't run without some tweaking!
  - Expected landmine, but I stepped on it anyway: why did I believe I could possibly get a naked TypeScript code snippet compiling in less than 17 hours? I wasted a few minutes trying.
- Day 02 (hacking in PowerShell)
  - Estimate: 60 minutes
  - Actual: 85 minutes
  - # Unexpected landmine: didn't understand part A problem until I had solved the problem wrong 2x-3x

* Day 01 (cheating)
  - Estimate: 5 minutes
  - Actual: 30 minutes
  - Unexpected landmine: cheating is harder than it seems. You have to work surprisingly hard to cheat, because code samples don't run without some tweaking!
  - Expected landmine, but I stepped on it anyway: why did I believe I could possibly get a naked TypeScript code snippet compiling in less than 17 hours? I wasted a few minutes trying.
* Day 02 (hacking in PowerShell)
  - Estimate: 60 minutes
  - Actual: 85 minutes
  - Unexpected landmine: didn't understand part A problem until I had solved the problem wrong 2x-3x
* Day 05 (cheating)
  - Estimate: 10 minutes
  - Actual: 4 minutes
