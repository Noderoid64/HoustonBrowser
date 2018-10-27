using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace HoustonBrowser
{
    public class MainWindow : Window
    {
        private Button checkButton;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            checkButton=this.Find<Button>("checkButton");
        }
    }
}