using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
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
using System.Windows.Controls.Primitives;


namespace DrawingApp.Controls
{
    /// <summary>
    /// Interaction logic for canvasSurface.xaml
    /// </summary>
    public partial class canvasSurface : UserControl
    {

        public canvasSurface()
        {
            InitializeComponent();
        }


        Point currentPoint = new Point();  
        private Rectangle rec;
        private Ellipse el;
        private Ellipse circ;
        

        private void Mouse_Down(object sender, MouseButtonEventArgs e)
        {
           
            var canvas = sender as Canvas;
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                currentPoint = e.GetPosition(canvas);

                if (MainWindow.RecatngleSelected.Equals(true))
                {
                    rec = new Rectangle
                    {
                        Stroke = MainWindow.Cbrush,
                        StrokeThickness = MainWindow.SelectedThickness
                    };

                    Canvas.SetLeft(rec, currentPoint.X);
                    Canvas.SetTop(rec, currentPoint.X);
                    canvas.Children.Add(rec);
                }

                else if (MainWindow.EllipseSelected.Equals(true))
                {
                    el = new Ellipse
                    {
                        Stroke = MainWindow.Cbrush,
                        StrokeThickness = MainWindow.SelectedThickness
                    };

                    Canvas.SetLeft(el, currentPoint.X);
                    Canvas.SetTop(el, currentPoint.X);
                    canvas.Children.Add(el);
                }


                else if (MainWindow.CircleSelected.Equals(true))
                {
                    circ = new Ellipse
                    {
                        Stroke = MainWindow.Cbrush,
                        StrokeThickness = MainWindow.SelectedThickness
                    };

                    Canvas.SetLeft(circ, currentPoint.X);
                    Canvas.SetTop(circ, currentPoint.X);
                    canvas.Children.Add(circ);
                }


            }

            }

        
        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            var canvas = sender as Canvas;
           
            if (e.LeftButton == MouseButtonState.Pressed && MainWindow.PencilSelected.Equals(true))
            {

                Line line = new Line();
             
                line.Stroke = MainWindow.Cbrush;
                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(canvas).X;
                line.Y2 = e.GetPosition(canvas).Y;
                line.StrokeThickness = MainWindow.SelectedThickness;

                currentPoint = e.GetPosition(canvas);
                canvas.Children.Add(line);
   
            }

            if (e.LeftButton == MouseButtonState.Pressed && MainWindow.EraserSelected.Equals(true))
            {

                Line line = new Line();
                this.Cursor = Cursors.Arrow;

                line.Stroke = Brushes.White;
                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(canvas).X;
                line.Y2 = e.GetPosition(canvas).Y;
                line.StrokeThickness = MainWindow.SelectedThickness;

                currentPoint = e.GetPosition(canvas);
                canvas.Children.Add(line);
     

            }

            if (MainWindow.RecatngleSelected.Equals(true))
            {
                if (e.LeftButton == MouseButtonState.Released || rec == null)
                    return;

                var pos = e.GetPosition(canvas);

                var x = Math.Min(pos.X, currentPoint.X);
                var y = Math.Min(pos.Y, currentPoint.Y);

                var w = Math.Max(pos.X, currentPoint.X) - x;
                var h = Math.Max(pos.Y, currentPoint.Y) - y;

                rec.Width = w;
                rec.Height = h;

                Canvas.SetLeft(rec, x);
                Canvas.SetTop(rec, y);
            }


            else if (MainWindow.EllipseSelected.Equals(true))
            {
                if (e.LeftButton == MouseButtonState.Released || el == null)
                    return;

                var pos = e.GetPosition(canvas);

                var x = Math.Min(pos.X, currentPoint.X);
                var y = Math.Min(pos.Y, currentPoint.Y);

                var w = Math.Max(pos.X, currentPoint.X) - x;
                var h = Math.Max(pos.Y, currentPoint.Y) - y;

                el.Width = w;
                el.Height = h;

                Canvas.SetLeft(el, x);
                Canvas.SetTop(el, y);
            }

            else if (MainWindow.CircleSelected.Equals(true))
            {
                if (e.LeftButton == MouseButtonState.Released || circ == null)
                    return;

                var pos = e.GetPosition(canvas);

                var x = Math.Min(pos.X, currentPoint.X);
                var y = Math.Min(pos.Y, currentPoint.Y);

                var w = Math.Max(pos.X, currentPoint.X) - x;
                var h = Math.Max(pos.Y, currentPoint.Y) - y;

                circ.Width = w;
                circ.Height = w;

                Canvas.SetLeft(circ, x);
                Canvas.SetTop(circ, y);
            }



        }

        private void Mouse_Up(object sender, MouseButtonEventArgs e)
         {
             var canvas = sender as Canvas;


             if (MainWindow.EllipseSelected.Equals(true))
              {
                  el = null;
              }

              if (MainWindow.RecatngleSelected.Equals(true))
              {

                  rec = null;

              }

              if (MainWindow.CircleSelected.Equals(true))
              {

                  circ = null;
              }




         }
    }
}  

