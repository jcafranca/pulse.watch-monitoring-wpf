using SweetAlertSharp.Enums;
using SweetAlertSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.Remoting.Contexts;

namespace FPOMonitoring.View.Window
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox 
    {
        private Brush brush = null;
        #region Private Fields
        private List<CancelEventHandler> _preCloseEvents = new List<CancelEventHandler>();
        private bool _isCloseable = true;
        private string _caption;
        #endregion
        public InputBox(string title)
        {
            InitializeComponent();
            _Title.Text = title;
            _Content.Focus();
        }
        #region Private Methods
        private bool RaiseCloseEvent()
        {
            var args = new CancelEventArgs();
            foreach (var preCloseEvent in _preCloseEvents)
            {
                preCloseEvent(this, args);

                if (args.Cancel)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
        #region Private Events
        private void Event_HideAnimation_Completed(object sender, EventArgs e)
        {
            if (_isCloseable)
            {
                return;
            }

            Close();
        }
        private void Event_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void Event_Closing(object sender, CancelEventArgs e)
        {
            var hideAnimation = this.FindResource("HideAnimation") as Storyboard;
            if (hideAnimation == null || !_isCloseable)
            {
                return;
            }

            e.Cancel = true;
            if (!RaiseCloseEvent())
            {
                _isCloseable = true;
                return;
            }

            _isCloseable = false;

            hideAnimation.Completed += Event_HideAnimation_Completed;
            hideAnimation.Begin(_Dialog);
        }
        #endregion

        #region Public Properties
        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;

                Title = _caption;

                NotifyPropertyChanged("Caption");
            }
        }
        public SweetAlertResult Result { get; private set; } = SweetAlertResult.CANCEL;
        public event CancelEventHandler PreClose
        {
            add => _preCloseEvents.Add(value);
            remove => _preCloseEvents.Remove(value);
        }
        #endregion

        #region Public Methods
        public new SweetAlertResult ShowDialog()
        {
            base.ShowDialog();
            return Result;
        }
        #endregion

        #region Public Events
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        private void _Content_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (brush is null) brush = _Content.BorderBrush;
                if (_Content.Text == "") SweetAlert.Show("Invalid", "Field cannot be blank.", msgImage: SweetAlertImage.ERROR);
                ButtonSave_Click(sender, e);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _Content.SelectAll();
        }

        private void _Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (brush != null) _Content.BorderBrush = brush;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            this.Result = SweetAlertResult.OK;
            Close();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Result = SweetAlertResult.CANCEL;
            Close();
        }
    }
}
