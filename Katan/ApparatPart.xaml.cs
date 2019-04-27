﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Katan.ViewModels;

namespace Katan
{
    /// <summary>
    /// Interaction logic for ApparatPart.xaml
    /// </summary>
    public partial class ApparatPart : Window
    {
        public ApparatPart()
        {
            InitializeComponent();
            DataContext = new KatanAdapterViewModel();
        }
    }
}