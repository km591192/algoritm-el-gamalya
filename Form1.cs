using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EL_GAMALYA_algoritm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int P;
        int G;
        int X;
        Alggamal alggamal = new Alggamal();
        Algoritm algoritm = new Algoritm();
       

        private void button1_Click(object sender, EventArgs e)
        {
            P = Convert.ToInt32(tbP.Text);
            G = Convert.ToInt32(tbG.Text);
            X = Convert.ToInt32(tbX.Text);
           //alggamal.crypt(P, G, X, textBox1.Text, tbinfo, textBox2);
          //  alggamal.cryptintcheck(P, G, X, 13, tbinfo, textBox2);
            algoritm.crypt(P, G, X, textBox1.Text, tbinfo, textBox2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            P = Convert.ToInt32(tbP.Text);
            G = Convert.ToInt32(tbG.Text);
            X = Convert.ToInt32(tbX.Text);
           // alggamal.decrypt(P, X, textBox2.Text,textBox3);
            //alggamal.decrypt(P, X,G, textBox2, textBox3);
            //alggamal.decryptintcheck(P, X, textBox2, textBox3);
            algoritm.decrypt(P, X, G, textBox2, textBox3);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*tbG.Clear();
            tbP.Clear();
            tbX.Clear();*/
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            tbinfo.Clear();
        }
    }
}
