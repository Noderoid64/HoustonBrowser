using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using HoustonBrowser.Core;

namespace HoustonBrowser
{
    public class MainWindow : Window, IUI
    {
        private Button checkButton;
        private Core.Core core;

        public MainWindow()
        {
            InitializeComponent();
            core = new Core.Core(this);
            core.onRender += Core_onRender;
        }

        public event Action<object, PointerPressedEventArgs> onMouseClick;
        public event Action<object, KeyEventArgs> onKeyDown;
        public event Action<object, object> onPageLoad;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            checkButton=this.Find<Button>("checkButton");

            this.PointerPressed += MainWindow_PointerPressed;
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, Avalonia.Input.KeyEventArgs e)
        {
            onKeyDown(sender, e);
        }

        private void MainWindow_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            onMouseClick(sender, e);
        }
        
        private void Core_onRender(object sender, object data)
        {
            
        }
    }
}