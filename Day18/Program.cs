using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Day18
{
    static class Program
    {
        private static MenuBar _menu;

        static void Main(string[] args)
        {
            _currentFrame = 0;
            _frames = Day18Solution.GetDummyFrames();

            Application.Init();
            _menu = new MenuBar(new MenuBarItem[]
            {
                new("_File", new MenuItem[]
                {
                    // TODO: learn about .NET console apps and why the only async thing I can get working is this. WHY!?!?!?
                    // ReSharper disable AsyncVoidLambda
                    new("_Play", "", async () => await PlayAnimation()),
                    new("_Step through", "", () => StepThroughAnimation()),
                    new("_Quit", "", () => Application.RequestStop()),
                })
            });
            Application.Top.Height = 101;
            Application.Top.Width = Math.Max(100, Application.Driver.Clip.Width);
            Application.Top.Add(_menu);
            Application.Run(Application.Top);
        }

        private static async Task PlayAnimation()
        {
            _menu.Visible = false;
            var frames = Day18Solution.GetDummyFrames();
            _progressIndicator = new Label(38,0, "");
            Application.Top.Add(_progressIndicator);

            await Task.Delay(500); // give a little delay before the first frame
            for (int i = 0; i < frames.Count(); i++)
            {
                _currentFrame = i;

                DrawCurrentFrame();
                await Task.Delay(400);
                if (i < frames.Count - 1)
                    HideCurrentFrame(); // don't remove the last frame
            }

            Application.Top.Remove(_progressIndicator);
            var border = new Border() { Padding = new Thickness(1), BorderThickness = new Thickness(1), BorderBrush = Color.White };
            var finalMessage = new Label(25, 25, "TODO: Place the final answer of Part A here.\nCTRL+Q to quit");
            finalMessage.Border = border;
            border.Child = finalMessage;
            Application.Top.Add(finalMessage);
            _menu.Visible = true;
        }

        private static int _currentFrame;
        private static List<string> _frames;
        private static Label _progressIndicator;
        private static Label _frameLabel;

        private static void StepThroughAnimation()
        {
            _menu.Visible = false;
            _progressIndicator = new Label(38,0, "");
            var nextButton = new Terminal.Gui.Button(0, 0, "_Next");
            nextButton.Clicked += () => NextFrame();
            nextButton.KeyDown += HandleAnimationHotkeys;
            Application.Top.Add(_progressIndicator);
            Application.Top.Add(nextButton);

            DrawCurrentFrame();
        }

        private static void HandleAnimationHotkeys(View.KeyEventEventArgs args)
        {
            if (args.KeyEvent.Key == Key.CursorLeft || args.KeyEvent.KeyValue == (uint)'h')
            {
                PreviousFrame();
            }
            else if (args.KeyEvent.Key == Key.CursorRight || args.KeyEvent.KeyValue == (uint)'l')
            {
                NextFrame();
            }
        }

        private static void NextFrame()
        {
            _currentFrame = Math.Clamp(_currentFrame + 1, 0, _frames.Count - 1);
            DrawCurrentFrame();
        }

        private static void PreviousFrame()
        {
            _currentFrame = Math.Clamp(_currentFrame - 1, 0, _frames.Count - 1);
            DrawCurrentFrame();
        }

        private static void DrawCurrentFrame()
        {
            _progressIndicator.Text = $"Frame {_currentFrame + 1}/{_frames.Count}";
            _frameLabel = new Label(0, 1, _frames[_currentFrame]);
            Application.Top.Add(_frameLabel);
        }

        private static void HideCurrentFrame()
        {
            Application.Top.Remove(_frameLabel);
        }
    }
}