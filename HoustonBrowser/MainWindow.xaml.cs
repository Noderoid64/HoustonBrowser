using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using HoustonBrowser.Controls;
using Avalonia.Media;
using System.Collections.Generic;
using Avalonia.Interactivity;
using HoustonBrowser.Core;

namespace HoustonBrowser
{
    public class MainWindow : Window, IUI
    {
        private Avalonia.Controls.Button backButton;
        private Avalonia.Controls.Button forwardButton;
        private Avalonia.Controls.Button refreshButton;
        private MyPanel drawPanel;
        private Core.Core core;

        public event EventHandler<PointerPressedEventArgs> onMouseClick;
        public event EventHandler<KeyEventArgs> onKeyDown;
        public event EventHandler<object> onPageLoad;

        public MainWindow()
        {
            InitializeComponent();

            backButton=this.Find<Avalonia.Controls.Button>("btnBack");
            forwardButton=this.Find<Avalonia.Controls.Button>("btnForward");
            refreshButton=this.Find<Avalonia.Controls.Button>("btnRefresh");  
            drawPanel=this.Find<MyPanel>("drawingCanvas");
            core = new Core.Core(this);
            
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);            
        }

        public void ShowAlert(string text)
        {
            Window alertWindow = new Window();
            alertWindow.Position=this.Position;
            alertWindow.Width = 150;
            alertWindow.Height=100;
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            TextBlock alertText = new TextBlock();
            alertText.Margin = new Thickness(10, 10, 0, 10);
            alertText.Text = text;
            stackPanel.Children.Add(alertText);
            Avalonia.Controls.Button okButton = new Avalonia.Controls.Button();
            okButton.Width = 30;
            okButton.Content ="Ok";
            stackPanel.Children.Add(okButton);
            alertWindow.Content = stackPanel;
            alertWindow.ShowDialog();
        }

    }
}