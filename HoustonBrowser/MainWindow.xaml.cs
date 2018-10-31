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
        private TextBlock checkString;
        private Core.Core core;

        public MainWindow()
        {
            InitializeComponent();
            core = new Core.Core(this);
            core.onRender += Core_onRender;
        }

        public event EventHandler<PointerPressedEventArgs> onMouseClick;
        public event EventHandler<KeyEventArgs> onKeyDown;
        public event EventHandler<object> onPageLoad;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            checkButton=this.Find<Button>("checkButton");
            checkString=this.Find<TextBlock>("checkString");

            this.PointerPressed += MainWindow_PointerPressed;
            this.KeyDown += MainWindow_KeyDown;     
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            onKeyDown(sender, e);
        }

        private void MainWindow_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            onMouseClick(sender, e);
        }
        
        private void Core_onRender(object sender, RenderEventArgs data)
        {
            checkString.Text=data.Data;
            checkString.InvalidateVisual();
        }
    }
}