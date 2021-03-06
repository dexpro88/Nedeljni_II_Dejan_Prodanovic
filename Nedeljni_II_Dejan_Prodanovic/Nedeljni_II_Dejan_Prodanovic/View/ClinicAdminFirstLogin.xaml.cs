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
    /// Interaction logic for ClinicAdminFirstLogin.xaml
    /// </summary>
    public partial class ClinicAdminFirstLogin : Window
    {
        public ClinicAdminFirstLogin()
        {
            InitializeComponent();
            
        }

        public ClinicAdminFirstLogin(tblClinicAdmin admin)
        {
            InitializeComponent();
            DataContext = new ClinicAdminFirstLoginViewModel(this, admin);
        }
    }
}
