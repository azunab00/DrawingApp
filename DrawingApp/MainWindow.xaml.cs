using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        private string tabName = "";
     
        public MainWindow()
        {
            InitializeComponent();
            SelectedColorRectangle.Fill = Brushes.Black;
            Cbrush = Brushes.Black;
            SelectedThickness = 1;
            PencilSelected = true;
           
           
        }

        private static Brush cbrush;
        public static Brush Cbrush
        {
            get { return cbrush; }
            set { cbrush = value; }
        }


        private static double selectedThickness;
        public static double SelectedThickness
        {
            get { return selectedThickness; }
            set { selectedThickness = value; }
        }

        private static bool pencilSelected;
        public static bool PencilSelected
        {
          get { return pencilSelected; }
          set { pencilSelected = value; }
        }

        private static bool ellipseSelected;
        public static bool EllipseSelected
        {
            get { return ellipseSelected; }
            set { ellipseSelected = value; }
        }

        private static bool rectangleSelected;
        public static bool RecatngleSelected
        {
            get { return rectangleSelected; }
            set { rectangleSelected = value; }
        }

        private static bool circleSelected;
        public static bool CircleSelected
        {
            get { return circleSelected; }
            set { circleSelected = value; }
        }

        private static bool eraserSelected;
        public static bool EraserSelected
        {
            get { return eraserSelected; }
            set { eraserSelected = value; }
        }


        private void NewWindow_Click(object sender, RoutedEventArgs e)
        {
           
            Input_Form form = new Input_Form();
            if (form.ShowDialog() == true)
                 tabName = form.Answer;

            
            if (tabName != "")
            {
                TabItem item = new TabItem() { Header = tabName };
                item.Content = new Controls.canvasSurface();
                Canvases.Items.Insert(Canvases.Items.Count, item);
                Canvases.SelectedIndex = Canvases.Items.Count - 1;
                tabName = "";
            }



        }


        private void ColorClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            SelectedColorRectangle.Fill = button.Background;
            Cbrush = button.Background;

          
          
        }


        private void onSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (sender as ComboBox).SelectedItem as ComboBoxItem;
            var number = Regex.Match(item.Content.ToString(), @"\d+").Value;
            SelectedThickness = int.Parse(number);
        }

        private void OnPencilSelect(object sender, RoutedEventArgs e)
        {
            PencilSelected = true;
            EllipseSelected = false;
            RecatngleSelected = false;
            CircleSelected = false;
            EraserSelected = false;
            
        }

        private void OnEllipseSelect(object sender, RoutedEventArgs e)
        {
            EllipseSelected = true;
            PencilSelected = false;
            RecatngleSelected = false;
            CircleSelected = false;
            EraserSelected = false;
        
        }

        private void OnRectangleSelect(object sender, RoutedEventArgs e)
        {
            RecatngleSelected = true;
            EllipseSelected = false;
            PencilSelected = false;
            CircleSelected = false;
            EraserSelected = false;
           
        }

        private void OnCircleSelect(object sender, RoutedEventArgs e)
        {
            CircleSelected = true;
            RecatngleSelected = false;
            EllipseSelected = false;
            PencilSelected = false;
            EraserSelected = false;
           
        }

        private void OnEraserSelect(object sender, RoutedEventArgs e)
        {
            EraserSelected = true;

            CircleSelected = false;
            RecatngleSelected = false;
            EllipseSelected = false;
            PencilSelected = false;
           

        }



        private void SaveWindow_Click(object sender, RoutedEventArgs e)
        {
            var tab = Canvases.SelectedItem as TabItem;
            var usercntr = tab.Content as Controls.canvasSurface;
            var canvas = usercntr.Crtanje;

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)canvas.ActualWidth,
             (int)canvas.ActualHeight, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(canvas);

            BitmapFrame frame = BitmapFrame.Create(rtb);
            var encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(frame);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            encoder.Save(ms);
            ms.Close();
            System.IO.File.WriteAllBytes(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\" + tab.Header.ToString()+".bmp", ms.ToArray());
            Confirmation_form confirm = new Confirmation_form();
            confirm.Show();
            
         
        }

        private void CloseTab(object sender, RoutedEventArgs e)
        {
            if(Canvases.Items.Count != 0)
                Canvases.Items.Remove(Canvases.SelectedItem);

        }
    }
}
