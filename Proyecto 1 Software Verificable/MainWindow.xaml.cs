using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Proyecto_1_Software_Verificable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime activeDate = DateTime.Now;
        public MainWindow()
        {
            InitializeComponent();
            UpdateCalendarGrid(activeDate);
            this.KeyDown += new KeyEventHandler(Arrow_Controls);
        }
        public void UpdateCalendarGrid(DateTime dateOfReference)
        {
            gridCalendar.Children.Clear();
            UpdateMonthYearLabel(dateOfReference);
            FillGridCalendar(gridCalendar, dateOfReference);
            ColourGridBorders(gridCalendar);
        }
        public void UpdateMonthYearLabel(DateTime date)
        {
            CultureInfo usEnglish = new CultureInfo("en-US");
            lblMonth.Content = date.ToString("MMMM", usEnglish).ToUpper();
            lblYear.Content = date.Year.ToString();
        }
        public void FillGridCalendar(Grid gridCalendar, DateTime dateWithinAMonth)
        {
            //x Coordate of each day on calendar grid
            int MondayPosition = 0;
            int TuesdayPosition = 1;
            int WendesdayPosition = 2;
            int ThursdayPosition = 3;
            int FridayPosition = 4;
            int SaturdayPosition = 5;
            int SundayPosition = 6;
            DateTime firstDayOfMonth = new DateTime(dateWithinAMonth.Year, dateWithinAMonth.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            int xCoordinateToInsertNumber = 0;
            int yCoordinateToInsertNumber = 0;
            int numberOfDay = 1;
            string dayOfWeekToStart = firstDayOfMonth.DayOfWeek.ToString();
            switch (dayOfWeekToStart)
            {
                case "Monday":
                    xCoordinateToInsertNumber = MondayPosition;
                    break;
                case "Tuesday":
                    xCoordinateToInsertNumber = TuesdayPosition;
                    break;
                case "Wednesday":
                    xCoordinateToInsertNumber = WendesdayPosition;
                    break;
                case "Thursday":
                    xCoordinateToInsertNumber = ThursdayPosition;
                    break;
                case "Friday":
                    xCoordinateToInsertNumber = FridayPosition;
                    break;
                case "Saturday":
                    xCoordinateToInsertNumber = SaturdayPosition;
                    break;
                case "Sunday":
                    xCoordinateToInsertNumber = SundayPosition;
                    break;
            }
            
            while (numberOfDay <= lastDayOfMonth.Day)
            {
                //Adding diffent color background to make stand out from days that aren't included in current month and are not weekends
                if (xCoordinateToInsertNumber != SaturdayPosition && xCoordinateToInsertNumber != SundayPosition)
                {
                    Rectangle rectangleDaysBackground = new Rectangle()
                    {
                        Fill = Brushes.White
                    };
                    Grid.SetColumn(rectangleDaysBackground, xCoordinateToInsertNumber);
                    Grid.SetRow(rectangleDaysBackground, yCoordinateToInsertNumber);
                    gridCalendar.Children.Add(rectangleDaysBackground);
                }
                else
                {
                    Rectangle rectangleWeekendBackground = new Rectangle()
                    {
                        Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#c1f7da")),
                    };
                    Grid.SetColumn(rectangleWeekendBackground, xCoordinateToInsertNumber);
                    Grid.SetRow(rectangleWeekendBackground, yCoordinateToInsertNumber);
                    gridCalendar.Children.Add(rectangleWeekendBackground);
                }

                //Adding number Labels to each day
                Label label = new Label()
                {
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Top,
                    FontSize = 25,
                    Content = numberOfDay
                };
                Grid.SetColumn(label, xCoordinateToInsertNumber);
                Grid.SetRow(label, yCoordinateToInsertNumber);
                gridCalendar.Children.Add(label);

                //Making the login of coordinates advance
                xCoordinateToInsertNumber++;
                if (xCoordinateToInsertNumber == gridCalendar.ColumnDefinitions.Count())
                {
                    yCoordinateToInsertNumber++;
                    xCoordinateToInsertNumber = 0;
                }

                numberOfDay++;
            }
        }
        public void ColourGridBorders(Grid grid)
        {
            int columnAmount = grid.ColumnDefinitions.Count();
            int rowAmount = grid.ColumnDefinitions.Count();
            int x_coordinate;
            int y_coordinate = 0;
            while (y_coordinate < rowAmount)
            {
                x_coordinate = 0;
                while (x_coordinate < columnAmount)
                {
                    Rectangle rectangleDayBorder = new Rectangle()
                    {
                        Fill = Brushes.Transparent,
                        Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#1f3861")),
                        StrokeThickness = 2
                    };
                    Grid.SetColumn(rectangleDayBorder, x_coordinate);
                    Grid.SetRow(rectangleDayBorder, y_coordinate);
                    grid.Children.Add(rectangleDayBorder);
                    x_coordinate++;
                }
                y_coordinate++;
            }
        }
        private void btnNextDay_Click(object sender, RoutedEventArgs e)
        {
            activeDate = activeDate.AddMonths(1);
            UpdateCalendarGrid(activeDate);
        }
        private void btnPrevDay_Click(object sender, RoutedEventArgs e)
        {
            activeDate = activeDate.AddMonths(-1);
            UpdateCalendarGrid(activeDate);
        }
        void Arrow_Controls(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    activeDate = activeDate.AddMonths(1);
                    UpdateCalendarGrid(activeDate);
                    break;
                case Key.Left:
                    activeDate = activeDate.AddMonths(-1);
                    UpdateCalendarGrid(activeDate);
                    break;
                case Key.Up:
                    activeDate = activeDate.AddYears(1);
                    UpdateCalendarGrid(activeDate);
                    break;
                case Key.Down:
                    activeDate = activeDate.AddYears(-1);
                    UpdateCalendarGrid(activeDate);
                    break;
            }
            
        }
    }
}
