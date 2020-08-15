﻿using Nedeljni_II_Dejan_Prodanovic.Model;
using Nedeljni_II_Dejan_Prodanovic.ViewModel;
using System;
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

namespace Nedeljni_II_Dejan_Prodanovic.View
{
    /// <summary>
    /// Interaction logic for EditDoctor.xaml
    /// </summary>
    public partial class EditDoctor : Window
    {
        public EditDoctor()
        {
            InitializeComponent();
        }
        public EditDoctor(vwClinicDoctor doctor)
        {
            InitializeComponent();
            DataContext = new EditDoctorViewModel(this, doctor);
        }
    }
}
