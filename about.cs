/*
Do not distribute, copy, modify, sell or reverse-engineer
this software without author's agreement.
Source code is provided for view only.
Source code and executable files
(C) 2023 n0nentity (https://github.com/n0n3nt1ty).
All rights reserved.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EBook_Reader_by_purplerain
{
    public partial class about : Form
    {
        public about()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/n0n3nt1ty/Kyokumi-Reader");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.com/users/459052435529269251");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://icons8.com");
        }
    }
}
