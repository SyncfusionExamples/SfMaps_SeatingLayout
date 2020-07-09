using Syncfusion.SfMaps.XForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Map_Custom_Shapes
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void MapsTicketBooking_ShapeSelectionChanged(object sender, ShapeSelectedEventArgs e)
        {
            ShapeFileLayer shapeLayer = sender as ShapeFileLayer;
            ShapeData data = e.Data as ShapeData;

            if (data != null)
            {
                if (data.SeatNumber == "1" || data.SeatNumber == "2" || data.SeatNumber == "8" || data.SeatNumber == "9")
                {
                    if (shapeLayer.SelectedItems.Contains(e.Data))
                        shapeLayer.SelectedItems.Remove(e.Data);
                }
                else
                {
                    UpdateSelection(shapeLayer);
                }
            }
        }

        void UpdateSelection(ShapeFileLayer shapeLayer)
        {
            string selected = "";
            if (shapeLayer.SelectedItems.Count == 0)
            {
                SelectedLabel.Text = selected;
                SelectedLabelCount.Text = " ";
                this.ClearButton.IsEnabled = false;
                this.ClearButton.Opacity = 0.5;
            }

            else
            {
                int count = 0;
                for (int i = 0; i < shapeLayer.SelectedItems.Count; i++)
                {
                    ShapeData data = shapeLayer.SelectedItems[i] as ShapeData;
                    count++;
                    if (shapeLayer.SelectedItems.Count == 1 || i == shapeLayer.SelectedItems.Count - 1)
                    {
                        selected += ("S" + data.SeatNumber);
                    }
                    
                    else
                    {
                        selected += ("S" + data.SeatNumber + ", ");
                    }

                    this.ClearButton.Opacity = 1;
                    this.ClearButton.IsEnabled = true;
                    SelectedLabel.Text = selected;
                }

                SelectedLabelCount.Text = "" + count;
            }
        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            var shapeLayer = this.Maps.Layers[0] as ShapeFileLayer;
            if (shapeLayer.SelectedItems.Count > 0)
            {
                shapeLayer.SelectedItems.Clear();
                SelectedLabel.Text = "";
                SelectedLabelCount.Text = "" + shapeLayer.SelectedItems.Count;
                this.ClearButton.IsEnabled = false;
                this.ClearButton.Opacity = 0.5;
            }
        }
    }

    public class ViewModel
    {
        public List<ShapeData> ShapeItems { get; set; }
        public ViewModel()
        {
            int totalSeats = 22;
            ShapeItems = new List<ShapeData>();
            for (int i = 1; i < totalSeats; i++)
            {
                ShapeItems.Add(new ShapeData("" + i));
            }
        }
    }

    public class ShapeData
    {
        public ShapeData(string seatNumber)
        {
            SeatNumber = seatNumber;
        }

        public string SeatNumber
        {
            get;
            set;
        }


    }
}
